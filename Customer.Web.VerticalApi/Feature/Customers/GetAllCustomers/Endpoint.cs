using MediatR;

namespace Customer.Web.VerticalApi.Feature.Customers.GetAllCustomers;

public static class Endpoint
{
    public static RouteGroupBuilder MapGetAllCustomersV2(this RouteGroupBuilder group)
    {
        group.MapGet("", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllCustomersQuery());
            return Results.Ok(result);
        }).WithName("GetAllCustomersV2");

        return group;
    }
}