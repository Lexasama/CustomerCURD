using MediatR;

namespace Customer.Web.Api.Feature.Customers.UpdateCustomer;

public interface IVerifyUserExist
{
}




public class VerifyUserExistBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IVerifyUserExist
{
    private readonly IVerifyUserExist _verifyUserExist;

    public VerifyUserExistBehavior(IVerifyUserExist verifyUserExist)
    {
        _verifyUserExist = verifyUserExist;
    }
    

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}