namespace Owin
{
    using IdentityServer3.Core.Configuration;
    using IdentityServer3.Core.Extensions;

    internal static class ConfigureAnonymousIdExtension
    {
        internal static IAppBuilder ConfigureAnonymousId(this IAppBuilder app, IdentityServerOptions identityServerOptions)
        {
            app.Use(
                async (ctx, next) =>
                    {
                        var anonCookie = ctx.Request.Cookies[identityServerOptions.AuthenticationOptions.CookieOptions.GetAnonymousCookieName()];

                        ctx.Environment.SetAnonymousId(anonCookie);

                        await next();
                    });

            return app;
        }
    }
}