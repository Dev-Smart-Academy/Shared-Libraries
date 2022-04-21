namespace SharedLibraries.Infrastructure.Email.SendGrid.Email;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using global::SendGrid;
using global::SendGrid.Helpers.Mail;

using Infrastructure.Email;

internal class SendGridService : IEmailService
{
    private readonly ISendGridClient sendGridClient;

    public SendGridService(ISendGridClient sendGridClient)
    {
        this.sendGridClient = sendGridClient;
    }

    public async Task<bool> Send(string from, IList<string> tos, string title, string body)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(from),
            Subject = title,
            HtmlContent = body
        };

        msg.AddTos(tos.Distinct().Select(x => new EmailAddress(x)).ToList());
        var response = await sendGridClient.SendEmailAsync(msg);

        return response.IsSuccessStatusCode;
    }
}
