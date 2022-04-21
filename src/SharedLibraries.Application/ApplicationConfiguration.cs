namespace SharedLibraries.Application;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedLibraries.Application.Behaviours;
using SharedLibraries.Application.Mapping;
using System;
using System.Reflection;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddCommonApplication(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .AddMediatR(assembly)
            .AddAutoMapperProfile(assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

    private static IServiceCollection AddAutoMapperProfile(
            this IServiceCollection services,
            Assembly assembly)
            => services
                .AddAutoMapper(
                    (_, config) => config
                        .AddProfile(new MappingProfile(assembly)),
                    Array.Empty<Assembly>());
}
