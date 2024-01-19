using Customer.Web.Api;
using Customer.Web.Api.Abstractions;

namespace Customer.Web.Shared.Services
{
    public sealed class LinkService : ILinkService
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public Link Generate(string endpoint, object? routeValues, string rel, string method)
        {
            return new Link(
                _linkGenerator.GetUriByName(
                    _httpContextAccessor.HttpContext,
                    endpoint,
                    routeValues
                ),
                rel, 
                method
            );
        }
    }
}