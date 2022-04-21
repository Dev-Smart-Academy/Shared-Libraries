namespace SharedLibraries.Infrastructure.Email.SendGrid.Email;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using Moq;
using Xunit;
using global::SendGrid;
using global::SendGrid.Helpers.Mail;
using System.Linq;
using FluentAssertions;

public class SendGridServiceSpecs
{
    [Fact]
    public async Task Send_Should_Return()
    {
        // Arrange
        var from = "service@academy.com";
        var tos = new List<string>()
        {
            "test@abv.bg",
            "test2@abv.bg",
            "test@abv.bg"
        };
        string title = "title";
        string body = "body";

        var sendGridClientMock = new Mock<ISendGridClient>();
        sendGridClientMock.Setup(x => x.SendEmailAsync(It.Is<SendGridMessage>(x=>
        x.From.Email == from && 
        x.Subject == title &&
        x.Personalizations.First().Tos.All(x=> tos.Contains(x.Email)) &&
        x.HtmlContent == body), It.IsAny<CancellationToken>()))
           .ReturnsAsync(new Response(System.Net.HttpStatusCode.OK, null, null));
        
        var sendGridService = new SendGridService(sendGridClientMock.Object);

        // Act
        var result = await sendGridService.Send(from,tos,title,body);

        // Assert
        result.Should().BeTrue();
        sendGridClientMock.Verify(x => x.SendEmailAsync(It.Is<SendGridMessage>(x =>
            x.From.Email == from &&
            x.Subject == title &&
            x.Personalizations.First().Tos.All(x => tos.Contains(x.Email)) &&
            x.HtmlContent == body), It.IsAny<CancellationToken>()), Times.Once());
    }
}
