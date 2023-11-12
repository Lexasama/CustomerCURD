using Dapper;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace Application.IntegrationTests;

public class DatabaseContainerPerTest
    : IAsyncLifetime
{
    // this is called for each test, since each test
    // instantiates a new class instance
    private readonly MsSqlContainer container =
        new MsSqlBuilder()
            .Build();

    private string connectionString = string.Empty;

    [Fact]
    public async Task Database_Can_Run_Query()
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        const int expected = 1;
        var actual = await connection.QueryFirstAsync<int>("SELECT 1");

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Database_Can_Select_DateTime()
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var actual = await connection.QueryFirstAsync<DateTime>("SELECT GETDATE()");
        Assert.IsType<DateTime>(actual);
    }
    //
    // [Fact]
    // public async Task Can_Store_Document_With_Marten()
    // {
    //     await using NpgsqlConnection connection = new(connectionString);
    //     var store = DocumentStore.For(options => {
    //         options.Connection(connectionString);
    //         options.AutoCreateSchemaObjects = AutoCreate.All;
    //     });
    //
    //     int id;
    //     {
    //         await using var session = store.IdentitySession();
    //         var person = new Person("Khalid");
    //         session.Store(person);
    //         await session.SaveChangesAsync();
    //
    //         id = person.Id;
    //     }
    //
    //     {
    //         await using var session = store.QuerySession();
    //         var person = session.Query<Person>().FindFirst(p => p.Id  id);
    //         Assert.NotNull(person);
    //     }
    // }

    public async Task InitializeAsync()
    {
        await container.StartAsync();
        connectionString = container.GetConnectionString();
    }

    public Task DisposeAsync() => container.StopAsync();
}