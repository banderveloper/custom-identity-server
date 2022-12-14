namespace IdentityServer.Api.Middleware.SAAuthentication;

// Send 401 http if we try to get to SA scope with invalid SA login and password in headers
public class SaAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public SaAuthenticationMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        string path = request.Path;

        // If we try to get to SA scope, and input login and password is invalid
        if (IsPathToSa(path) && HeadersContainsInvalidSa(request.Headers))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { errpr = "Invalid SA login or password" });
        }
        else
        {
            // if not SA scope or correct SA login&pass
            await _next.Invoke(context);
        }
    }

    // Is path startsWith /sa (system administrator scope)
    private bool IsPathToSa(string path)
    {
        return path.StartsWith("/sa");
    }

    // Request headers contains invalid SA login or password, got from launch arguments
    private bool HeadersContainsInvalidSa(IHeaderDictionary headers)
    {
        headers.TryGetValue("sa_login", out var headerLogin);
        headers.TryGetValue("sa_password", out var headerPassword);

        var trueLogin = _config.GetValue<string>("SA_LOGIN");
        var truePassword = _config.GetValue<string>("SA_PASSWORD");

        return headerLogin != trueLogin || headerPassword != truePassword;
    }
}