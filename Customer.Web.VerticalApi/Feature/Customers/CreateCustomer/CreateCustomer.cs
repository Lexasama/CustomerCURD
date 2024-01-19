using Customer.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.VerticalApi.Feature.Customers.CreateCustomer;


    public class CreateCusterCommand : IRequest<int>
    {
        [FromBody]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
    
    
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCusterCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<int> Handle(CreateCusterCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Create(request.FirstName, request.LastName, request.Email,
                request.DateOfBirth);
            return customer.Id;
        }
    }
    
    
public class CreateCustomerCommandValidator : AbstractValidator<CreateCusterCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }

public static class CustomerCreateEndpoint
{
    public static RouteGroupBuilder MapGetAllCustomersV2(this RouteGroupBuilder group)
    {
        group.MapPost("", async (ISender sender, CreateCustomerViewModel viewModel) =>
        {
            var result = await sender.Send(new CreateCusterCommand());
            return Results.Ok(result);
        }).WithName("CreateCustomersV2");

        return group;
    }
}