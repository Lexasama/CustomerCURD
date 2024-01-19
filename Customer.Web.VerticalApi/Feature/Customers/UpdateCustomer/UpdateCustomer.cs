using Customer.Domain.Models;
using Customer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Api.Feature.Customers.UpdateCustomer;

public class UpdateCustomer
{
    
}

public record UpdateCustomerCommand(string FistName, string LastName, string Email, [FromRoute] int Id) : IRequest<Unit>, IVerifyUserExist


public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
       


        customer.FirstName = request.Fistname;
        customer.LastName = request.LastName;
        customer.Email = request.Email;

        await _customerRepository.UpdateAsync(customer);

        return Unit.Value;
    }
}

//recuperer le customer id 
// verifier si il existe 
// si il existe on le met a jour