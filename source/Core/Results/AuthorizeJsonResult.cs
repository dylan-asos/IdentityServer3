namespace IdentityServer3.Core.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using IdentityServer3.Core.Models;

    public class AuthorizeJsonResult : IHttpActionResult
    {
        protected readonly AuthorizeResponse _response;

        protected readonly HttpRequestMessage _request;

        public AuthorizeJsonResult(AuthorizeResponse response, HttpRequestMessage request)
        {
            _response = response;
            _request = request;
        }

        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var payload = JwtTokenResponse.ConvertFromAuthorizationResponse(_response);

            var response = _request.CreateResponse(HttpStatusCode.OK, payload);

            return Task.FromResult(response);
        }
    }
}