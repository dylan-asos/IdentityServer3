namespace IdentityServer3.Core.Services
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using IdentityServer3.Core.Models;

    /// <summary>
    /// The anonymous claims provider is responsible for determining which claims to include in tokens when a 
    /// request for a anon token is processed
    /// </summary>
    public interface IAnonymousClaimsProvider
    {
        /// <summary>
        /// Returns a default set of claims for an anonymous identity or access token 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetAnonymousClaims(Client client, IEnumerable<Scope> scopes);
    }
}