using Customer.Web.Api.Feature.Customers.GetAllCustomers;
using Customer.Web.Api.Feature.Customers.GetCustomer;
using Customer.Web.VerticalApi.Feature.Customers.GetAllCustomers;

namespace Customer.Web.Api.Feature.Customers;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpointsV2(this IEndpointRouteBuilder app)
    {
        app.MapGroup("/api/v2/customers")
            .MapGetCustomerV2()
            .MapGetAllCustomersV2()
            .WithName("CustomerEndpointsV2");

        return app;
    }
}