using Customer.Web.Api.Validation.Customer;
using FluentValidation.AspNetCore;

namespace Customer.Web.Api.Validation
{
    public static class ValidationExtensions 
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(config => { config.DisableDataAnnotationsValidation = true; });
            services.AddCustomerValidators();
        }
    }
}