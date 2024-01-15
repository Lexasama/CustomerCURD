using MediatR;

namespace Customer.Web.Api.Feature.Customers.GetAllCustomers;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetAllCustomersV2(this RouteGroupBuilder group)
    {
        group.MapGet("", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllCustomersCommand());
            return Results.Ok(result);
        }).WithName("GetAllCustomersV2");

        return group;
    }
}