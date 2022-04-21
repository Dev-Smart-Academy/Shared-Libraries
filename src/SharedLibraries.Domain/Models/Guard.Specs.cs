namespace SharedLibraries.Domain.Models;

using FluentAssertions;
using Xunit;

public class GuardSpecs
{
    [Fact]
    public void AgainstEmptyString_Should_Throw_When_Value_Is_Null()
    {
        // Arrange
        string? value = null;

        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.AgainstEmptyString<MockException>(value));
        exception.Error.Should().Be($"Value cannot be null ot empty.");
    }

    [Fact]
    public void AgainstEmptyString_Should_Pass_When_Valid()
    {
        // Arrange
        string value = "test";

        // Act
        // Assert
        Guard.AgainstEmptyString<MockException>(value);
    }

    [Fact]
    public void ForStringLength_Should_Throw_When_Value_Is_Null()
    {
        // Arrange
        string? value = null;

        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.ForStringLength<MockException>(value, 1, 2));
        exception.Error.Should().Be($"Value cannot be null ot empty.");
    }

    [Fact]
    public void ForStringLength_Should_Throw_When_Invalid_Range()
    {
        // Arrange
        string? value = "as";

        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.ForStringLength<MockException>(value, 3, 5));
        exception.Error.Should().Be($"Value must have between 3 and 5 symbols.");
    }

    [Fact]
    public void ForStringLength_Should_Pass_When_Valid()
    {
        // Arrange
        string? value = "asty";

        // Act
        // Assert
        Guard.ForStringLength<MockException>(value, 3, 5);
    }

    [Fact]
    public void AgainstOutOfRange_Int_Should_Throw_When_Invalid_Range()
    {
        // Arrange
        int value = 16;

        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.AgainstOutOfRange<MockException>(value, 3, 5));
        exception.Error.Should().Be($"Value must be between 3 and 5.");
    }

    [Fact]
    public void AgainstOutOfRange_Int_Should_Pass_When_Valid()
    {
        // Arrange
        int value = 4;

        // Act
        // Assert
        Guard.AgainstOutOfRange<MockException>(value, 3, 5);
    }

    [Fact]
    public void AgainstOutOfRange_Decimal_Should_Throw_When_Invalid_Range()
    {
        // Arrange
        decimal value = 16m;

        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.AgainstOutOfRange<MockException>(value, 3, 5));
        exception.Error.Should().Be($"Value must be between 3 and 5.");
    }

    [Fact]
    public void AgainstOutOfRange_Decimal_Should_Pass_When_Valid()
    {
        // Arrange
        decimal value = 4m;

        // Act
        // Assert
        Guard.AgainstOutOfRange<MockException>(value, 3, 5);
    }

    [Fact]
    public void ForValidUrl_Should_Throw_When_IsWellFormedUriString_False()
    {
        // Arrange
        string url = "2013.05.29_14:33:41";


        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.ForValidUrl<MockException>(url));
        exception.Error.Should().Be($"Value must be a valid URL.");
    }

    [Fact]
    public void ForValidUrl_Should_Throw_When_Url_Is_TooLong()
    {
        // Arrange
        string url = new string('a', Guard.MaxUrlLength) + ".com";


        // Act
        // Assert
        var exception = Assert.Throws<MockException>(() => Guard.ForValidUrl<MockException>(url));
        exception.Error.Should().Be($"Value must be a valid URL.");
    }

    [Fact]
    public void ForValidUrl_Should_Pass_When_Valid_Url()
    {
        // Arrange
        string url = "http://www.abv.com";


        // Act
        // Assert
        Guard.ForValidUrl<MockException>(url);
    }

    public class MockException : BaseDomainException
    {

    }
}
