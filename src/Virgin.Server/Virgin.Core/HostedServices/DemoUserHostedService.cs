using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Virgin.Core.Services.Interfaces;

namespace Virgin.Core.HostedServices
{
    public sealed class DemoUserHostedService : IHostedService, IDisposable
    {
        private const string DemoUserId = "7728cbbf-e4e5-47de-bbbb-ab5888fc6889";

        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public DemoUserHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(15), TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DemoUserHostedService>>();

            try
            {
                var customerRedirectService = scope.ServiceProvider.GetRequiredService<ICustomerRedirectService>();
                var dataList = await customerRedirectService.GetCustomerRedirectsAsync(DemoUserId).ConfigureAwait(false);

                foreach (var item in dataList)
                {
                    await customerRedirectService.DeleteCustomerRedirectNoCheckAsync(item.Id).ConfigureAwait(false);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(DemoUserHostedService)}: {ex.Message}", ex);
            }

        }
        public void Dispose() => _timer?.Dispose();
    }
}
