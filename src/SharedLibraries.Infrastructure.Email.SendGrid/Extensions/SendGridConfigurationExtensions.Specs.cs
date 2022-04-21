namespace SharedLibraries.Infrastructure.Email.SendGrid.Extensions;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using FluentAssertions;
using Xunit;

public class SendGridConfigurationExtensionsSpecs
{
    [Fact]
    public void GetSendGridSettings_Should_Return()
    {
        // Arrange
        var apiKey = Guid.NewGuid().ToString();

        var myConfiguration = new Dictionary<string, string>
        {
            {"SendGridSettings:ApiKey", apiKey},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        // Act
        var result = configuration.GetSendGridSettings();

        // Assert
        result.ApiKey.Should().Be(apiKey);
    }
}
