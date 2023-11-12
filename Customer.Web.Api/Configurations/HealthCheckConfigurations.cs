using Customer.Web.Api.Configurations.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Customer.Web.Api.Configurations
{
    public static class HealthCheckConfigurations
    {
        public static void AddHealthCheckConfigurations(this IServiceCollection services,
            string connectionString)
        {
            services
                .AddHealthChecks()
                .AddCheck<CustomHealthCheck>("Api-HealthCheck")
                .AddSqlServer( 
                    connectionString, name : "Database-HealthCheck"
                    );
            
            services
                .AddHealthChecksUI(options => { options.AddHealthCheckEndpoint("Roomies Health Check", "/health"); })
                .AddInMemoryStorage();
        }
        
        
        
        public static void UseHealthCheckConfigurations(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/health-ui";
                options.ApiPath = "/health-ui-api";
            });
            
        }
    }
}