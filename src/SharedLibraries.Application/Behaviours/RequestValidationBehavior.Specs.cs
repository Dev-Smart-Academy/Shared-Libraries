namespace SharedLibraries.Application.Behaviours;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using Xunit;

using Exceptions;

public class RequestValidationBehaviorSpecs
{
    [Fact]
    public async Task Should_Thrown_When_Entity_Is_Not_Valid()
    {
        // Arrange
        MockRequest mockRequest = new MockRequest();
        var mockValidator = new Mock<IValidator<MockRequest>>();
        var failures = new List<ValidationFailure>();
        failures.Add(new ValidationFailure("Name", "fail"));

        mockValidator.Setup(x => x.Validate(It.IsAny<ValidationContext<MockRequest>>()))
            .Returns(new ValidationResult(failures));

        RequestValidationBehavior<MockRequest, MockResponse> requestValidationBehavior = new RequestValidationBehavior<MockRequest, MockResponse>(new List<IValidator<MockRequest>>() { mockValidator.Object });
        RequestHandlerDelegate<MockResponse> next = () => Task.FromResult(new MockResponse());


        // Act
        // Assert
        var message = await Assert.ThrowsAsync<ModelValidationException>(async () => await requestValidationBehavior.Handle(mockRequest, default, next));
        Assert.Equal("Name", message.Errors.Keys.First());
        Assert.Equal("fail", message.Errors.Values.First()[0]);
    }

    [Fact]
    public async Task Should_Pass_When_Is_Valid()
    {
        // Arrange
        MockRequest mockRequest = new MockRequest();
        var mockValidator = new Mock<IValidator<MockRequest>>();

        mockValidator.Setup(x => x.Validate(It.IsAny<ValidationContext<MockRequest>>()))
            .Returns(new ValidationResult());

        var mockNext = new Mock<RequestHandlerDelegate<MockResponse>>();

        RequestValidationBehavior<MockRequest, MockResponse> requestValidationBehavior = new RequestValidationBehavior<MockRequest, MockResponse>(new List<IValidator<MockRequest>>() { mockValidator.Object });

        // Act
        await requestValidationBehavior.Handle(mockRequest, default, mockNext.Object);

        // Assert
        mockNext.Verify(f => f(), Times.Once);
    }


    public class MockRequest : IRequest<MockResponse>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class MockResponse
    {

    }
}
