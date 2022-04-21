namespace SharedLibraries.Infrastructure.Email.SendGrid.Extensions;

using Microsoft.Extensions.Configuration;

using Configuration;

internal static class SendGridConfigurationExtensions
{
    internal static SendGridSettings GetSendGridSettings(this IConfiguration configuration)
        => configuration.GetSection(nameof(SendGridSettings)).Get<SendGridSettings>();
}
