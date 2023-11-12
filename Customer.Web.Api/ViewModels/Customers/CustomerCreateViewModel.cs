namespace Customer.Web.Api.ViewModels.Customers;

public class CustomerCreateViewModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}