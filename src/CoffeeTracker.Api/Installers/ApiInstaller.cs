using Asp.Versioning;
using CoffeeTracker.Api.OpenApi;
using CoffeeTracker.Api.Routes;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoffeeTracker.Api.Installers;

public static class ApiInstaller
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCors();

        services.AddAuthorization();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddMiddleware(this WebApplication app)
    {
        // Required before UseSwaggerUI to ensure correct definitions are available to swagger.
        app.MapCoffeeTrackerEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.DescribeApiVersions())
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }

        app.UseHttpsRedirection();

        app.UseCors(options =>
        {
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

        app.UseAuthorization();

        return app;
    }
}
