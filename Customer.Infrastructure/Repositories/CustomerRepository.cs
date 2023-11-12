using Customer.Domain.Models;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Entities.Customer;
using Dapper;

namespace Customer.Infrastructure.repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;

        public CustomerRepository(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<CustomerDomain>> FindAll()
        {
            await using var connection = _sqlConnectionFactory.Create();
            const string sql = "SELECT * FROM Customers";

            var result = await connection.QueryAsync<CustomerEntity>(sql);
            return result.Select(ConvertToDomain);
        }

        public async Task<CustomerDomain?> FindById(int id)
        {
            await using var connection = _sqlConnectionFactory.Create();
            const string sql = """
                               SELECT *
                               FROM Customers
                               WHERE Id = @CustomerId
                               """;
            var result = await connection.QuerySingleOrDefaultAsync<CustomerEntity>(sql,
                new { CustomerId = id });
            return result is null ? null : ConvertToDomain(result);
        }

        public async Task<CustomerDomain> Create(string firstname, string lastName, string email, DateTime dateOfBirth)
        {
            await using var connection = _sqlConnectionFactory.Create();
            const string sql = """
                               INSERT INTO CUSTOMERS (FirstName, LastName, Email, DateOfBirth)
                               VALUES (@FirstName, @LastName, @Email, @DateOfBirth);
                               SELECT * FROM CUSTOMERS WHERE Id =  SCOPE_IDENTITY();
                               """;
            var created = await connection.QueryFirstAsync<CustomerEntity>(sql,
                new
                {
                    firstname,
                    lastName,
                    email,
                    dateOfBirth
                });
            return ConvertToDomain(created);
        }

        public async Task Update(int id, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            await using var connection = _sqlConnectionFactory.Create();
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
                    id,
                    firstName,
                    lastName,
                    email,
                    dateOfBirth
                });
        }

        public async Task Delete(int id)
        {
            await using var conn = _sqlConnectionFactory.Create();
            const string sql = """
                               DELETE FROM CUSTOMERS
                                      WHERE Id = @CustomerId;
                               """;
            await conn.ExecuteAsync(sql, new { CustomerId = id });
        }

        private static CustomerDomain ConvertToDomain(CustomerEntity customerEntity)
        {
            return new CustomerDomain
            {
                Id = customerEntity.Id,
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName,
                Email = customerEntity.Email,
                DateOfBirth = customerEntity.DateOfBirth
            };
        }
    }
}