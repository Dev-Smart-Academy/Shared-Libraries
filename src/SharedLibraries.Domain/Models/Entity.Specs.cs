namespace SharedLibraries.Domain.Models;

using FluentAssertions;
using Xunit;

public class EntitySpecs
{
    [Fact]
    public void Entities_With_Equal_Ids_Should_Be_Equal()
    {
        // Arrange
        var first = new TestEntity(1);
        var second = new TestEntity(1);

        // Act
        var result = first == second;

        // Arrange
        result.Should().BeTrue();
    }

    [Fact]
    public void Entities_With_Different_Ids_Should_Not_Be_Equal()
    {
        // Arrange
        var first = new TestEntity(1);
        var second = new TestEntity(2);

        // Act
        var result = first == second;

        // Arrange
        result.Should().BeFalse();
    }

    private class TestEntity : Entity<int>
    {
        public TestEntity(int id)
        {
            this
              .GetType()
              .BaseType!
              .GetProperty(nameof(Entity<int>.Id))!
              .GetSetMethod(true)!
              .Invoke(this, new object[] { id });
        }
    }
}
