namespace IdentityServer3.Core.Configuration.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Security.Claims;

    using IdentityServer3.Core.Extensions;

    using Microsoft.Owin;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class AnonymousCookie
    {
        readonly IOwinContext context;

        readonly IdentityServerOptions identityServerOptions;

        protected internal AnonymousCookie(IOwinContext ctx, IdentityServerOptions options)
        {
            context = ctx;
            identityServerOptions = options;
        }

        public virtual void IssueAnonymousId(ClaimsIdentity identity)
        {
            var path = context.Request.Environment.GetIdentityServerBasePath().CleanUrlPath();
            DateTimeOffset offset = DateTimeHelper.UtcNow.Add(identityServerOptions.AuthenticationOptions.CookieOptions.ExpireTimeSpan);

            context.Response.Cookies.Append(GetCookieName(), identity.GetSubjectId(), new CookieOptions { Path = path, Secure = true, Expires = offset.UtcDateTime });
        }

        private string GetCookieName()
        {
            return identityServerOptions.AuthenticationOptions.CookieOptions.GetAnonymousCookieName();
        }
    }
}