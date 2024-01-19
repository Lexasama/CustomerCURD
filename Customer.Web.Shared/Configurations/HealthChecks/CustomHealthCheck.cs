using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Customer.Web.Api.Configurations.HealthChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return HealthCheckResult.Healthy("App is up and running");
            }
            catch (Exception e)
            {
                return
                    HealthCheckResult.Unhealthy(
                        exception: e,
                        description: "App is down");
            }
        }
    }
}