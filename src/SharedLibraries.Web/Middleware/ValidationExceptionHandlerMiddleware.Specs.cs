namespace SharedLibraries.Web.Middleware;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Http;

using Moq;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

using Application.Exceptions;

public class ValidationExceptionHandlerMiddlewareSpecs
{
    [Fact]
    public async Task Invoke_Should_Return_BadRequest_When_ModelValidationException()
    {
        // Arrange
        var validationFailure = new ValidationFailure("name", "error");
        RequestDelegate next = (ctx) => Task.FromException(new ModelValidationException(new List<ValidationFailure>() { validationFailure }));
        ValidationExceptionHandlerMiddleware validationExceptionHandlerMiddleware = new ValidationExceptionHandlerMiddleware(next);
        var mockedContext = new Mock<HttpContext>();
        var mockedResponse = new MockedResponse();
        mockedContext.Setup(x => x.Response).Returns(mockedResponse);

        // Act
        await validationExceptionHandlerMiddleware.Invoke(mockedContext.Object);

        // Assert
        mockedResponse.ContentType.Should().Be("application/json");
        mockedResponse.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Invoke_Should_Return_BadRequest_When_NullReferenceException()
    {
        // Arrange
        RequestDelegate next = (ctx) => Task.FromException(new NullReferenceException());
        ValidationExceptionHandlerMiddleware validationExceptionHandlerMiddleware = new ValidationExceptionHandlerMiddleware(next);
        var mockedContext = new Mock<HttpContext>();
        var mockedResponse = new MockedResponse();
        mockedContext.Setup(x => x.Response).Returns(mockedResponse);

        // Act
        await validationExceptionHandlerMiddleware.Invoke(mockedContext.Object);

        // Assert
        mockedResponse.ContentType.Should().Be("application/json");
        mockedResponse.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Invoke_Should_Return_NotFound_When_NotFoundException()
    {
        // Arrange
        RequestDelegate next = (ctx) => Task.FromException(new NotFoundException("key","value"));
        ValidationExceptionHandlerMiddleware validationExceptionHandlerMiddleware = new ValidationExceptionHandlerMiddleware(next);
        var mockedContext = new Mock<HttpContext>();
        var mockedResponse = new MockedResponse();
        mockedContext.Setup(x => x.Response).Returns(mockedResponse);

        // Act
        await validationExceptionHandlerMiddleware.Invoke(mockedContext.Object);

        // Assert
        mockedResponse.ContentType.Should().Be("application/json");
        mockedResponse.StatusCode.Should().Be(404);
    }

    private class MockedResponse : HttpResponse
    {
        public MockedResponse()
        {
            Body = new MemoryStream();
        }

        public override HttpContext HttpContext => throw new NotImplementedException();

        public override int StatusCode { get; set; }

        public override IHeaderDictionary Headers => throw new NotImplementedException();

        public override Stream Body { get; set; }
        public override long? ContentLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string ContentType { get; set; } = "";

        public override IResponseCookies Cookies => throw new NotImplementedException();

        public override PipeWriter BodyWriter => PipeWriter.Create(Body);

        public override bool HasStarted => true;

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}