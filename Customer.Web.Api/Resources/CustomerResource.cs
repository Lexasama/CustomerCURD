namespace Customer.Web.Api.Resources
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

        public static implicit operator CustomerResource(Domain.Models.CustomerDomain customerDomain)
        {
            return new CustomerResource(
                customerDomain.Id,
                customerDomain.FirstName,
                customerDomain.LastName,
                customerDomain.Email,
                customerDomain.DateOfBirth);
        }

        public static CustomerResource From(Domain.Models.CustomerDomain customerDomain)
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