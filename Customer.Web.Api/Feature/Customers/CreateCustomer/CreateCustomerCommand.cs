// using Customer.Domain.Abstractions;
// using Customer.Domain.Repositories;
// using Customer.Web.Api.Validation;
// using FluentValidation;
// using MediatR;
//
// namespace Customer.Web.Api.Feature.Customers;
//
// public record CreateCustomerCommand(string FirstName, string LastName, string Email, DateTime DateOfBirth)
//     : IRequest<ResultT<Customer, ValidationFailed>>;
//
// public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ResultT<Customer, ValidationFailed>>
// {
//     private readonly ICustomerRepository _customerRepository;
//     private readonly IValidator<Customer> _validator;
//
//     public async Task<ResultT<Customer, ValidationFailed>> Handle(CreateCustomerCommand request,
//         CancellationToken cancellationToken)
//     {
//         var cus = new Customer(request.FirstName, request.LastName,
//             request.Email, request.DateOfBirth);
//         var validationResult = await _validator.ValidateAsync(cus, cancellationToken);
//
//         if (!validationResult.IsValid)
//         {
//             return new ValidationFailed(validationResult.Errors);
//         }
//
//         await _customerRepository.Create(request.FirstName, request.LastName,
//             request.Email, request.DateOfBirth);
//         return cus;
//     }
// }