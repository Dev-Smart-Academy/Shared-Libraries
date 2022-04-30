namespace SharedLibraries.Web.ParameterTransformers;

using FluentAssertions;
using Xunit;

public class SlugifyParameterTransformerSpecs
{
    [Fact]
    public void Should_Return_Null_When_Null()
    {
        // Arrange
        SlugifyParameterTransformer slugifyParameterTransformer = new SlugifyParameterTransformer();

        // Act
        var result = slugifyParameterTransformer.TransformOutbound(null);

        // Assert
        result.Should().BeNull();
    }

    [Theory]
    [InlineData("TestTest","test-test")]
    [InlineData("TestTestTest", "test-test-test")]
    [InlineData("testt", "testt")]
    public void Should_Return_TransformedParameter(string actual, string expected)
    {
        // Arrange
        SlugifyParameterTransformer slugifyParameterTransformer = new SlugifyParameterTransformer();

        // Act
        var result = slugifyParameterTransformer.TransformOutbound(actual);

        // Assert
        result.Should().Be(expected);
    }
}
