using Customer.Domain.Models;

namespace Customer.Domain.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Models.CustomerDomain>> GetAll();
        Task<Models.CustomerDomain> FindOne(int id);
        Task Delete(int id);

        Task<CustomerDomain> Create(string firstname, string lastName, string email, DateTime dateOfBirth);
        Task Update(int id, string firstName, string lastName, string email, DateTime dateOfBirth);
    }
}