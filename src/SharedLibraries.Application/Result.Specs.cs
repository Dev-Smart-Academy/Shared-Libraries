namespace SharedLibraries.Application;

using System.Collections.Generic;
using Xunit;

public class ResultSpecs
{
    [Fact]
    public void Result_Should_Be_False_When_Failure()
    {
        // Arrange
        // Act
        Result result = Result.Failure(new List<string>());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Result_Should_Be_False_When_Error()
    {
        // Arrange
        // Act
        Result result = "Error";

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Result_Should_Be_False_When_Errors()
    {
        // Arrange
        // Act
        Result result = new List<string>() { "Error" };

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Result_Should_Be_True_When_Success()
    {
        // Arrange
        // Act
        Result result = true;

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void Result_Error_Should_Be_Set_Result_False()
    {
        // Arrange
        // Act
        Result result = false;

        // Assert
        Assert.Equal("Unsuccessful operation.", result.Errors[0]);
    }

    [Fact]
    public void Result_Should_Be_True_When_Value_Set()
    {
        // Arrange
        // Act
        Result<int> result = 6;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Result_Should_Be_True_When_SuccessWith()
    {
        // Arrange
        // Act
        Result<int> result = Result<int>.SuccessWith(6);

        // Assert
        Assert.True(result);
    }
}
