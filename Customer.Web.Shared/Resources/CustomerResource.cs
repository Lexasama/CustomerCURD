using Customer.Domain.Models;
using Customer.Web.Api;

namespace Customer.Web.Shared.Resources
{
    public record CustomerResource
    (
        int Id,
        string FirstName,
        string LastName,
        string Email,
        DateTime DateOfBirth)
    {
        public List<Link> Links { get; set; } = new();

        public static implicit operator CustomerResource(CustomerDomain  customerDomain)
        {
            return new CustomerResource(
                customerDomain.Id,
                customerDomain.FirstName,
                customerDomain.LastName,
                customerDomain.Email,
                customerDomain.DateOfBirth);
        }

        public static CustomerResource From(CustomerDomain customerDomain)
        {
            return new CustomerResource(
                customerDomain.Id,
                customerDomain.FirstName, 
                customerDomain.LastName,
                customerDomain.Email,
                customerDomain.DateOfBirth);
        }
    }
}