namespace SharedLibraries.Infrastructure.Email.SendGrid;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using global::SendGrid.Extensions.DependencyInjection;

using SharedLibraries.Infrastructure.Email;
using Extensions;
using Email;

public static class SendGridConfiguration
{
    public static IServiceCollection AddSendGridEMailService(this IServiceCollection services,
        IConfiguration configuration)
        => services.AddSendGrid(options =>
            options.ApiKey = configuration.GetSendGridSettings().ApiKey)
            .Services.AddScoped<IEmailService, SendGridService>();
}
