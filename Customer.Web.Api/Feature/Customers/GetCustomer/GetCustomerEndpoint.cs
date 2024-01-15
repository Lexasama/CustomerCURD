using MediatR;

namespace Customer.Web.Api.Feature.Customers.GetCustomer;

public static class GetCustomerEndpoint
{
    public static RouteGroupBuilder MapGetCustomerV2(this RouteGroupBuilder group)
    {
        group.MapGet("{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetCustomerQuery(id));
            return Results.Ok(result);
        }).WithName("GetCustomerV2");

        return group;
    }
}