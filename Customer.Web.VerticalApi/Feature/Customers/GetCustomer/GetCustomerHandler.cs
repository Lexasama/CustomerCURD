using Customer.Domain.Exceptions;
using Customer.Domain.Repositories;
using Customer.Web.Api.Resources;
using MediatR;

namespace Customer.Web.Api.Feature.Customers.GetCustomer;


public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerResource?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async  Task<CustomerResource?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var resource = await _customerRepository.FindById(request.Id);
        
        if (resource is null)
        {
            throw new NotFoundEntityAppException("Customer not found");
        }
        return CustomerResource.From(resource);
    }
}