namespace SharedLibraries.Domain.Models;

using FluentAssertions;
using Xunit;

public class ValueObjectSpecs
{
    [Fact]
    public void ValueObjects_With_Equal_Properties_Should_Be_Equal()
    {
        // Arrange
        var first = new TestValueObject();
        var second = new TestValueObject();

        // Act
        var result = first == second;

        // Arrange
        result.Should().BeTrue();
    }

    [Fact]
    public void ValueObjects_With_Different_Properties_Should_Not_Be_Equal()
    {
        // Arrange
        var first = new TestValueObject();
        var second = new TestValueObject2();

        // Act
        var result = first == second;

        // Arrange
        result.Should().BeFalse();
    }

    private class TestValueObject : ValueObject
    {
    }

    private class TestValueObject2 : ValueObject
    {
        public string Test { get; } = default!;
    }
}
