namespace SharedLibraries.Web.Attributes;

using FluentAssertions;
using Xunit;

public class AuthorizeAdministratorAttributeSpecs
{
    [Fact]
    public void Role_Should_Be_Administrator()
    {
        // Arrange
        // Act
        AuthorizeAdministratorAttribute authorizeAdministratorAttribute = new AuthorizeAdministratorAttribute();

        // Assert
        authorizeAdministratorAttribute.Roles.Should().Be(Domain.Models.ModelConstants.Common.AdministratorRoleName);
    }
}

