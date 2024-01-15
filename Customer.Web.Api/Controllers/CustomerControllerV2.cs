using Customer.Web.Api.Feature.Customers;
using Customer.Web.Api.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Api.Controllers;

public class CustomerControllerV2 : BaseController
{
    private readonly IMediator _mediator;

    public CustomerControllerV2(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [HttpGet("v2/{id:int}", Name = "GetCustomerV2")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<CustomerResource>> FindOne(int id)
    // {
    //     var command = new GetCustomerQuery(id);
    //
    //     var result = await _mediator.Send(command);
    //
    //     return result ?? null;
    // }
}