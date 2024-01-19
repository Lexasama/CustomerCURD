using Customer.Domain.Models;
using Customer.Domain.Repositories;
using MediatR;

namespace Customer.Web.VerticalApi.Feature.Customers.GetAllCustomers;


public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDomain>>;

public class Handler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDomain>>
{
    
    private readonly ICustomerRepository _customerRepository;

    public Handler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }


    public async Task<IEnumerable<CustomerDomain>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.FindAll();
    }
}

