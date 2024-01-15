using Customer.Web.Api.Resources;
using MediatR;

namespace Customer.Web.Api.Feature.Customers.GetCustomer;

public record GetCustomerQuery(int Id) : IRequest<CustomerResource?>;

