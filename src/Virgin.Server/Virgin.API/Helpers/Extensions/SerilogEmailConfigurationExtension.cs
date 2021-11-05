using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;

namespace Virgin.API.Helpers.Extensions
{
    internal static class SerilogEmailConfigurationExtension
    {
        internal static LoggerConfiguration EmailConfigurationExtension(
            this LoggerConfiguration loggerConfiguration,
            IConfigurationRoot configuration
        )
        {
            var emailSinkSettings = configuration.GetSection("EmailSinkSettings").Get<EmailSinkSettings>();

            return loggerConfiguration.WriteTo.Email(
                emailSinkSettings.ConnectionInfo,
                emailSinkSettings.OutputTemplate,
                emailSinkSettings.RestrictedToMinimumLevel
            );
        }
    }

    internal class EmailSinkSettings
    {
        public ConfigCredidentialsEmailConnectionInfo ConnectionInfo { get; set; }
        public string OutputTemplate { get; set; }
        public LogEventLevel RestrictedToMinimumLevel { get; set; }
    }

    internal class ConfigCredidentialsEmailConnectionInfo : EmailConnectionInfo
    {
        public ConfigCredidentialsEmailConnectionInfo()
        {
            NetworkCredentials = new NetworkCredential();
        }
    }
}