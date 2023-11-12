using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;

public abstract class BasIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;

    protected BasIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
    }
}