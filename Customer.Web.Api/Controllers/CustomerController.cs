using Customer.Domain.Services;
using Customer.Web.Api.Abstractions;
using Customer.Web.Api.Resources;
using Customer.Web.Api.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Api.Controllers;

public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;
    private readonly ILinkService _linkService;

    public CustomerController(ICustomerService customerService, ILinkService linkService)
    {
        _customerService = customerService;
        _linkService = linkService;
    }

    [HttpGet(Name = "GetCustomers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerResource>>> FindAll()
    {
        var result = await _customerService.GetAll();
        return Ok(CreateResource(result));
    }


    [HttpGet("{id:int}", Name = "GetCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResource>> FindOne(int id)
    {
        return CreateResource(await _customerService.FindOne(id));
    }

    [HttpPost(Name = "CreateCustomer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CustomerResource>> Create(
        [FromBody] CustomerCreateViewModel customerCreateViewModel)
    {
        var created = await _customerService.Create(
            customerCreateViewModel.FirstName,
            customerCreateViewModel.LastName,
            customerCreateViewModel.Email,
            customerCreateViewModel.DateOfBirth
        );
        var resource = CreateResource(created);
        return Created(resource.Links[0].Href, resource);
    }

    [HttpPut("{id:int}", Name = "UpdateCustomer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResource>> Update(int id,
        [FromBody] CustomerUpdateViewModel customerCreateViewModel)
    {
        await _customerService.Update(
            id,
            customerCreateViewModel.FirstName,
            customerCreateViewModel.LastName,
            customerCreateViewModel.Email,
            customerCreateViewModel.DateOfBirth
        );
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteCustomer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.Delete(id);
        return NoContent();
    }

    private CustomerResource CreateResource(Domain.Models.CustomerDomain customerDomain)
    {
        var resource = CustomerResource.From(customerDomain);
        resource.Links.Add(_linkService.Generate(
            "GetCustomer", new { id = customerDomain.Id },
            "self",
            "GET"));
        resource.Links.Add(_linkService.Generate("UpdateCustomer", new { id = customerDomain.Id },
            "update-customer",
            "PUT"));
        resource.Links.Add(_linkService.Generate("DeleteCustomer", new { id = customerDomain.Id },
            "delete-customer",
            "DELETE"));

        return resource;
    }

    private IEnumerable<CustomerResource> CreateResource(IEnumerable<Domain.Models.CustomerDomain> customers)
    {
        return customers.Select(CreateResource).ToList();
    }
}