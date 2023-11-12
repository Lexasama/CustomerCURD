using Customer.Domain.Exceptions;
using Customer.Domain.Models;
using Customer.Domain.Repositories;

namespace Customer.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerDomain>> GetAll()
    {
        return await _repository.FindAll();
    }

    public async Task<CustomerDomain> FindOne(int id)
    {
        var customer = await _repository.FindById(id);
        if (customer is null)
            throw new NotFoundEntityAppException(nameof(customer), id);

        return customer;
    }

    public async Task Delete(int id)
    {
        await FindOne(id);
        await _repository.Delete(id);
    }

    public async Task<CustomerDomain> Create(string firstname, string lastName, string email, DateTime dateOfBirth)
    {

        return await _repository.Create(firstname, lastName, email, dateOfBirth);

    }

    public async Task Update(int id, string firstName, string lastName, string email, DateTime dateOfBirth)
    {

        await FindOne(id);
        await _repository.Update( id,  firstName, lastName,  email,  dateOfBirth);
    }
}