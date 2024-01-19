using Customer.Web.Api.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Api.Feature.Customers.GetCustomer;

public record GetCustomerQuery([FromRoute] int Id) : IRequest<CustomerResource?>;