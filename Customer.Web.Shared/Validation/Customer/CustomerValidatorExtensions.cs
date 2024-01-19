using Customer.Web.Api.ViewModels.Customers;
using Customer.Web.Shared.ViewModels.Customers;
using FluentValidation;

namespace Customer.Web.Api.Validation.Customer
{
    public static class CustomerValidatorExtensions
    {
        public static void AddCustomerValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CustomerCreateViewModel>, CustomerCreateValidator>();
        }
    }
}