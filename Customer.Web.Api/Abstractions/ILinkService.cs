namespace Customer.Web.Api.Abstractions
{
    public interface ILinkService
    {
        Link Generate(string endpoint, object? routeValues, string rel, string method);
    }
}