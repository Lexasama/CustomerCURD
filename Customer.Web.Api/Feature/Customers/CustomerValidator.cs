using Customer.Domain.Models;
using Customer.Domain.Repositories;
using FluentValidation;

namespace Customer.Web.Api.Feature.Customers;

public class CustomerValidator : AbstractValidator<CustomerDomain>
{
    
    private readonly ICustomerRepository _customerRepository;

    public CustomerValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(x => x.FirstName)
            .NotEmpty();
    }
}