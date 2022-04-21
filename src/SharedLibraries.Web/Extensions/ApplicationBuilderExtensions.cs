namespace SharedLibraries.Web.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Infrastructure.Persistence;
using Middleware;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebService(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
        =>  app
              .UseExceptionHandling(env)
              .UseSwagger(env)
              .UseValidationExceptionHandler()
              .UseHttpsRedirection()
              .UseRouting()
              .UseCors(options => options
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod())
              .UseAuthentication()
              .UseAuthorization()
              .UseEndpoints(endpoints => endpoints
                  .MapHealthChecks()
                  .MapControllers());

    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }

        return app;
    }

    public static IApplicationBuilder UseSwagger(
      this IApplicationBuilder app,
      IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }


    public static IApplicationBuilder Initialize(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var initializers = serviceScope.ServiceProvider.GetServices<IDbInitializer>();

        foreach (var initializer in initializers)
        {
            initializer.Initialize();
        }

        return app;
    }
}
