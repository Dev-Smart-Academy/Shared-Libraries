namespace SharedLibraries.Domain.Models;

using System;
using Xunit;

public class MessageSpecs
{
    [Fact]
    public void Message_Should_Be_Set_Successfully()
    {
        // Arrange
        Type expectedType = typeof(decimal);
        decimal value = 5.5m;

        // Act
        Message message = new Message(value);

        // Assert
        Assert.Equal(value, message.Data);
        Assert.Equal(expectedType, message.Type);
        Assert.False(message.Published);
    }

    [Fact]
    public void Message_Should_Be_Published()
    {
        // Arrange
        Type expectedType = typeof(decimal);
        decimal value = 5.5m;

        // Act
        Message message = new Message(value);
        message.MarkAsPublished();

        // Assert
        Assert.Equal(value, message.Data);
        Assert.Equal(expectedType, message.Type);
        Assert.True(message.Published);
    }
}
