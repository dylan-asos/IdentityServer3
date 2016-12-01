namespace IdentityServer3.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using IdentityModel;

    using IdentityServer3.Core.Extensions;
    using IdentityServer3.Core.Models;

    public class DefaultAnonymousClaimsProvider : IAnonymousClaimsProvider
    {
        public IEnumerable<Claim> GetAnonymousClaims(Client client, IEnumerable<Scope> scopes)
        {
            var claims = new List<Claim>
                             {
                                 new Claim(Constants.ClaimTypes.AuthenticationMethod, Constants.Authentication.AnonymousAuthenticationType),
                                 new Claim(Constants.ClaimTypes.Subject, CurrentAnonymousId ?? Guid.NewGuid().ToString("N")),
                                 new Claim(Constants.ClaimTypes.ClientId, client.ClientId),
                                 new Claim(Constants.ClaimTypes.IdentityProvider, "site"),
                                 new Claim(Constants.ClaimTypes.AuthenticationTime, DateTimeOffsetHelper.UtcNow.ToEpochTime().ToString())
                             };

            claims.AddRange(scopes.Select(scope => new Claim(Constants.ClaimTypes.Scope, scope.Name)));

            return claims;
        }

        /// <summary>
        ///     Existing anonymous id if present
        /// </summary>
        public string CurrentAnonymousId { get; set; }
    }
}