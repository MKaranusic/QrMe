using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Virgin.API.Helpers.Extensions;
using Virgin.Core.Services;
using Virgin.Core.Services.Interfaces;
using Virgin.Infrastructure.DAL;

namespace Virgin.API.Helpers
{
    public class StartupHelpers
    {
        public static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("VirginDbConnection");

            services.AddDbContext<VirginDbContext>(
                options => options.UseSqlServer(connectionString));
        }

        public static void RegisterDomainServices(IServiceCollection services, LogEventLevel appLogLvl)
        {
            #region ClaimsPrincipal DI for UserContextService

            services.AddHttpContextAccessor();
            services.AddTransient(x => x.GetService<IHttpContextAccessor>().HttpContext?.User);
            services.AddScoped<IUserContextService, UserContextService>();

            #endregion ClaimsPrincipal DI for UserContextService

            if (appLogLvl is LogEventLevel.Debug)
            {
                services.AddProxiedTransient<ICustomerQRService, CustomerQRService>();
                services.AddProxiedTransient<ICustomerRedirectService, CustomerRedirectService>();
                services.AddProxiedTransient<IQrRedirectService, QrRedirectService>();
            }
            else
            {
                services.AddTransient<ICustomerQRService, CustomerQRService>();
                services.AddTransient<ICustomerRedirectService, CustomerRedirectService>();
                services.AddTransient<IQrRedirectService, QrRedirectService>();
            }
        }
    }
}