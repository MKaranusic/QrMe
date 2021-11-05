using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Virgin.Shared.Interceptors;

namespace Virgin.API.Helpers.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddProxiedTransient<TInterface, TImplementation>(this IServiceCollection services)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            services.AddTransient<IInterceptor, LoggingInterceptor<TImplementation>>();
            services.AddTransient<TImplementation>();
            services.AddTransient(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptor = serviceProvider.GetService<IInterceptor>();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptor);
            });
        }
    }
}