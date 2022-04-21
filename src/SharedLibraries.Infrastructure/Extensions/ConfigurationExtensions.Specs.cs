namespace SharedLibraries.Infrastructure.Extensions;

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using FluentAssertions;
using Xunit;

using Application.Settings;

public class ConfigurationExtensionsSpecs
{
    [Fact]
    public void GetDefaultConnectionString_Should_Return() 
    {
        // Arrange
        var myConfiguration = new Dictionary<string, string>
        {
            {"ConnectionStrings:DefaultConnection", "test"},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        // Act
        var result = configuration.GetDefaultConnectionString();

        // Assert
        result.Should().Be("test");
    }

    [Fact]
    public void GetCronJobsConnectionString_Should_Return()
    {
        // Arrange
        var myConfiguration = new Dictionary<string, string>
        {
            {"ConnectionStrings:CronJobsConnection", "test"},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        // Act
        var result = configuration.GetCronJobsConnectionString();

        // Assert
        result.Should().Be("test");
    }

    [Fact]
    public void GetMessageQueueSettings_Should_Return()
    {
        // Arrange
        var myConfiguration = new Dictionary<string, string>
        {
            {"MessageQueueSettings:Host", "host"},
            {"MessageQueueSettings:UserName", "username"},
            {"MessageQueueSettings:Password", "password"},
        };

        MessageQueueSettings messageQueueSettings = new MessageQueueSettings("host", "username", "password");


        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        // Act
        var result = configuration.GetMessageQueueSettings();

        // Assert
        result.Host.Should().Be(messageQueueSettings.Host);
        result.UserName.Should().Be(messageQueueSettings.UserName);
        result.Password.Should().Be(messageQueueSettings.Password);
    }
}
