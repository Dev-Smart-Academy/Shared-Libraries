namespace SharedLibraries.Web.Extensions;

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FluentAssertions;
using Xunit;

using SharedLibraries.Application;

public class ResultExtensionsSpecs
{
    [Fact]
    public async Task ToActionResult_Should_Return_NotFoundResult_When_Result_Is_Null()
    {
        // Arrange
        Task<int?> task = Task.FromResult<int?>(null);

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
        result.Value.Should().BeNull();
    }

    [Fact]
    public async Task ToActionResult_Should_Return_When_Result_Is_Not_Null()
    {
        // Arrange
        Task<int?> task = Task.FromResult<int?>(5);

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Result.Should().BeNull();
        result.Value.Should().Be(5);
    }

    [Fact]
    public async Task ToActionResult_Should_Return_BadRequestObjectResult_When_Not_Succeeded_WithoutData()
    {
        // Arrange
        var errors = new List<string>() { "Error!" };
        Task<Result> task = Task.FromResult<Result>(Result.Failure(errors));

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        result.As<BadRequestObjectResult>().Value.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public async Task ToActionResult_Should_Return_OkResult_When_Succeeded_WithoutData()
    {
        // Arrange
        Task<Result> task = Task.FromResult<Result>(Result.Success);

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task ToActionResult_Should_Return_BadRequestObjectResult_When_Not_Succeeded_WithData()
    {
        // Arrange
        var errors = new List<string>() { "Error!" };
        Task<Result<int?>> task = Task.FromResult<Result<int?>>(Result<int?>.Failure(errors));

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        result.Result.As<BadRequestObjectResult>().Value.Should().BeEquivalentTo(errors);
    }

    [Fact]
    public async Task ToActionResult_Should_Return_When_Data_Is_Not_Null_WithData()
    {
        // Arrange
        var errors = new List<string>() { "Error!" };
        Task<Result<int?>> task = Task.FromResult<Result<int?>>(Result<int?>.SuccessWith(5));

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Should().BeOfType<ActionResult<int?>>();
        result.Value.Should().Be(5);
    }

    [Fact]
    public async Task ToActionResult_Should_Return_FileStreamResult()
    {
        // Arrange
        MemoryStream ms = new MemoryStream(new byte[] { 5,6 });
        Task<Stream> task = Task.FromResult<Stream> (ms);

        // Act
        var result = await task.ToActionResult();

        // Assert
        result.Should().BeOfType<FileStreamResult>();
        result.As<FileStreamResult>().FileStream.Should().BeSameAs(ms);
        result.As<FileStreamResult>().ContentType.Should().Be(System.Net.Mime.MediaTypeNames.Image.Jpeg);
    }
}
