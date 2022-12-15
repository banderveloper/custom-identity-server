using System.Net;
using System.Text.Json;
using IdentityServer.Application.Common.Exceptions;

namespace IdentityServer.Api.Middleware.CustomExceptionHandler;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case AlreadyExistsException:
                code = HttpStatusCode.Conflict;
                break;
            case IncorrectPasswordException:
                code = HttpStatusCode.BadRequest;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
            result = JsonSerializer.Serialize(new { errpr = exception.Message });

        return context.Response.WriteAsync(result);
    }
}