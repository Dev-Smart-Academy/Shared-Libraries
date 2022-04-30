namespace SharedLibraries.Web;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using FluentValidation.AspNetCore;

using SharedLibraries.Web.ParameterTransformers;

using Application.Contracts;
using Services;

public static class WebConfiguration
{
    public static IServiceCollection AddCommonWebComponents(
        this IServiceCollection services,
        Type applicationConfigurationType)
    {
        services.AddHealthChecks();
        services
            .AddEndpointsApiExplorer()
            .AddScoped<ICurrentUser, CurrentUserService>()
            .AddSwaggerGen(c=> {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });
            })
            .AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            })
            .AddFluentValidation(validation => validation
                .RegisterValidatorsFromAssemblyContaining(applicationConfigurationType))
            .AddNewtonsoftJson();

        services
            .Configure<ApiBehaviorOptions>(options => options
                .SuppressModelStateInvalidFilter = true);

        return services;
    }
}
