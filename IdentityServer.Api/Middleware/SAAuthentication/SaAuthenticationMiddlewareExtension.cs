namespace IdentityServer.Api.Middleware.SAAuthentication;

public static class SaAuthenticationMiddlewareExtension
{
    public static IApplicationBuilder UseSaAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SaAuthenticationMiddleware>();
    }
}