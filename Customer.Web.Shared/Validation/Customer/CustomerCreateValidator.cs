using Customer.Web.Api.ViewModels.Customers;
using Customer.Web.Shared.ViewModels.Customers;
using FluentValidation;

namespace Customer.Web.Api.Validation.Customer
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateViewModel>
    {
        public CustomerCreateValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.DateOfBirth).NotEmpty();
        }
    }
}