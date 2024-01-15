using Customer.Domain.Models;
using Customer.Domain.Repositories;
using MediatR;

namespace Customer.Web.Api.Feature.Customers.GetAllCustomers;


public record GetAllCustomersCommand : IRequest<IEnumerable<CustomerDomain>>;

public class Handler : IRequestHandler<GetAllCustomersCommand, IEnumerable<CustomerDomain>>
{
    
    private readonly ICustomerRepository _customerRepository;

    public Handler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }


    public async Task<IEnumerable<CustomerDomain>> Handle(GetAllCustomersCommand request, CancellationToken cancellationToken)
    {
        return await _customerRepository.FindAll();
    }
}

