namespace Customer.Web.Api.Feature.Customers;

public class Customer
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public Customer(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
}