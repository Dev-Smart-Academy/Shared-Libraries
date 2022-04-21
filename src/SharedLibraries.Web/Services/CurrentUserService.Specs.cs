namespace SharedLibraries.Web.Services;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using FluentAssertions;
using Moq;

using Xunit;

public class CurrentUserServiceSpecs
{
    [Fact]
    public void CurrentUserService_Should_Throw_When_User_Is_Not_Set()
    {
        // Arrange
        Mock<IHttpContextAccessor> httpContextAccessor = new Mock<IHttpContextAccessor>();

        // Act
        // Assert
        var exception = Assert.Throws<InvalidOperationException>(() => { var obj = new CurrentUserService(httpContextAccessor.Object); });
        exception.Message.Should().Be("This request does not have an authenticated user.");
    }

    [Fact]
    public void CurrentUserService_Should_Set_UserId_When_User_Is_Set()
    {
        // Arrange
        string userId = "ID";

        var user = new ClaimsPrincipal(new List<ClaimsIdentity>()
        {
            new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            })
        });

        var httpContext = new Mock<HttpContext>();
        httpContext.Setup(x=> x.User).Returns(user);

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext.Object);

        // Act
        var currentUserService = new CurrentUserService(httpContextAccessor.Object);

        // Assert
        currentUserService.UserId.Should().Be(userId);
    }
}
