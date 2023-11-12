using Customer.Infrastructure;
using Customer.Web.Api.Abstractions;
using Customer.Web.Api.Resources;
using Customer.Web.Api.ViewModels.Customers;
using Dapper;

namespace Customer.Web.Api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("customers");

            var linkService =  builder.ServiceProvider.GetService<ILinkService>();

            group.MapGet("", async (SqlConnectionFactory sqlConnectionFactory) =>
            {
                await using var connection = sqlConnectionFactory.Create();
                const string sql = "SELECT * FROM Customers";
                var customers = await connection.QueryAsync<Customer.Domain.Models.CustomerDomain>(sql);

                foreach (var customer in customers)
                {
                    AddLinksToCustomer(customer);
                }

                return Results.Ok(customers);
            }).WithName("GetCustomers");

            group.MapGet("{id:int}",
                async (int id,
                    SqlConnectionFactory sqlConnectionFactory) =>
                {
                    await using var connection = sqlConnectionFactory.Create();
                    const string sql = """
                                       SELECT *
                                       FROM Customers
                                       WHERE Id = @CustomerId
                                       """;
                    var customer = await connection.QuerySingleOrDefaultAsync<CustomerResource>(sql,
                        new { CustomerId = id });

                    if (customer is null)
                    {
                        return Results.NotFound();
                    }

                    AddLinksToCustomer(customer);
                    return Results.Ok(customer);
                }).WithName("GetCustomer");

            group.MapPost("", async (CustomerCreateViewModel customer, SqlConnectionFactory sqlConnectionFactory) =>
            {
                await using var connection = sqlConnectionFactory.Create();
                const string sql = """
                                   INSERT INTO CUSTOMERS (FirstName, LastName, Email, DateOfBirth)
                                   VALUES (@FirstName, @LastName, @Email, @DateOfBirth);
                                   """;

                await connection.ExecuteAsync(sql,
                    customer);
                return Results.Ok();
            }).WithName("CreateCustomer");

            group.MapPut("{id:int}",
                async (int id, CustomerUpdateViewModel customer, SqlConnectionFactory sqlConnectionFactory) =>
                {
                    await using var connection = sqlConnectionFactory.Create();
                    const string sql = """
                                       UPDATE CUSTOMERS
                                       SET FirstName = @FirstName,
                                           LastName = @LastName,
                                           Email = @Email,
                                           DateOfBirth = @DateOfBirth
                                       WHERE Id = @id;
                                       """;

                    await connection.ExecuteAsync(sql,
                        new
                        {
                            Id = id,
                            customer.FirstName,
                            customer.LastName,
                            customer.Email,
                            customer.DateOfBirth
                        });
                    return Results.NoContent();
                }).WithName("UpdateCustomer");

            group.MapDelete("{id:int}", async (int id, SqlConnectionFactory connectionFactory) =>
            {
                await using var conn = connectionFactory.Create();
                const string sql = """
                                   DELETE FROM CUSTOMERS
                                          WHERE Id = @CustomerId;
                                   """;
                await conn.ExecuteAsync(sql, new { CustomerId = id });

                return Results.NoContent();
            }).WithName("DeleteCustomer");

            void AddLinksToCustomer(CustomerResource customer)
            {
                
                customer.Links.Add(linkService.Generate(
                    "GetCustomer", new { id = customer.Id },
                    "self",
                    "GET"));
                customer.Links.Add(linkService.Generate("UpdateCustomer", new { id = customer.Id },
                    "update-customer",
                    "PUT"));
                customer.Links.Add(linkService.Generate("DeleteCustomer", new { id = customer.Id },
                    "delete-customer",
                    "DELETE"));
            }

            void AddLinksToCustomers(IEnumerable<CustomerResource> customers)
            {
                foreach (var customer in customers)
                {
                    AddLinksToCustomer(customer);
                }
            }
        }
    }
}