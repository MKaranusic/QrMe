using Microsoft.Extensions.Configuration;
using Serilog;
using Virgin.API.Helpers.Extensions;
using Virgin.Shared.Exceptions;

namespace Virgin.API.Helpers
{
    internal static class SerilogFactory
    {
        internal static LoggerConfiguration Build()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .Build();

            if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environment.Development)
                return new LoggerConfiguration().ReadFrom.Configuration(config);

            return new LoggerConfiguration().ReadFrom.Configuration(config)
                                                     .EmailConfigurationExtension(config)
                                                     .Filter.ByExcluding(logEvent => logEvent.Exception?.InnerException is DoNotLogException);
        }
    }
}