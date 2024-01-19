using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Customer.Web.Shared.Configurations.Swagger;

public static class SwaggerConfigurationExtensions
{
    
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Care.Customer", Version = "v1" });
            swaggerGenOptions.IncludeXmlComments(Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "xml"));
           
        });
        
        return services;
    }
    
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Care.Customer v1");
            c.RoutePrefix = "";
        });

        return app;
    }
}