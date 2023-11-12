using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;

namespace Application.IntegrationTests;

public class GetOne
{
    
    public class HttpTest : IAsyncLifetime
    {
        private readonly IContainer container;

        public HttpTest()
        {
            container = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPortBinding(1444, 1433)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SQLCMDUSER", "satest")
                .WithPassword("yourStrong(!)Password")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1444))
                .Build();
        }

        [Fact]
        public async Task Can_Call_Endpoint()
        {
            var httpClient = new HttpClient();
            var requestUri =
                new UriBuilder(
                    Uri.UriSchemeHttp,
                    container.Hostname,
                    container.GetMappedPublicPort(8080),
                    "uuid"
                ).Uri;
            var guid = await httpClient.GetStringAsync(requestUri);
            Assert.True(Guid.TryParse(guid, out _));
        }
        public Task InitializeAsync()
            => container.StartAsync();
        public Task DisposeAsync()
            => container.DisposeAsync().AsTask();
    }
}