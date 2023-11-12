using Customer.Domain.Models;

namespace Customer.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<CustomerDomain>> FindAll();

        public Task<CustomerDomain?> FindById(int id);
        public Task Delete(int id);

        public Task<CustomerDomain> Create(string firstname, string lastName, string email, DateTime dateOfBirth);
        public Task Update(int id, string firstName, string lastName, string email, DateTime dateOfBirth);
    }
}