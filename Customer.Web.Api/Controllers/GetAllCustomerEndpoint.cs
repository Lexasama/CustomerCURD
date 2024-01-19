using Customer.Domain.Models;
using Customer.Domain.Services;
using Customer.Web.Api.Abstractions;
using Customer.Web.Shared.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Api.Controllers;

public class GetAllCustomerEndpoint : BaseController
{
    private readonly ICustomerService _customerService;
    private readonly ILinkService _linkService;

    public GetAllCustomerEndpoint(ICustomerService customerService, ILinkService linkService)
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

    
    private CustomerResource CreateResource( CustomerDomain customerDomain)
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