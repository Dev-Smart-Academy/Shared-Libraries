namespace SharedLibraries.Application.Exceptions;

using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using FluentValidation.Results;
using Xunit;

public class ModelValidationExceptionSpecs
{
    [Fact]
    public void Errors_Should_Be_Set_Correctly()
    {
        // Arrange
        var errors = new List<ValidationFailure>();
        errors.Add(new ValidationFailure("name","test"));
        errors.Add(new ValidationFailure("name", "test2"));


        // Act
        var modelValidationException = new ModelValidationException(errors);

        // Assert
        modelValidationException.Errors.Count.Should().Be(1);
        modelValidationException.Errors.Keys.First().Should().Be("name");
        modelValidationException.Errors["name"].Length.Should().Be(2);
        modelValidationException.Errors["name"][0].Should().Be("test");
        modelValidationException.Errors["name"][1].Should().Be("test2");
    }
}
