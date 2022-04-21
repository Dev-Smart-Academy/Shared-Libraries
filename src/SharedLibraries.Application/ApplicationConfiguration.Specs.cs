namespace SharedLibraries.Application;

using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Xunit;

public class ApplicationConfigurationSpecs
{
    [Fact]
    public void AddCommonApplication_Should_Register_ValidationBehavior()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        var services = serviceCollection
            .AddCommonApplication(this.GetType().Assembly);

        // Assert
        services.Any(x => x.ServiceType == typeof(IPipelineBehavior<,>));
    }
}
