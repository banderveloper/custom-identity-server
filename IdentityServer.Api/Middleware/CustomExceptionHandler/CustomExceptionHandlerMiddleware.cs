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
        var statusCode = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new
                {
                    description = notFoundException.Message, // text of exception
                    entityName = notFoundException.EntityName, // entity class name, that is not found  
                    notFoundValue = notFoundException.NotFoundValue // value of entity, that is not found
                });
                break;
            
            case AlreadyExistsException alreadyExistsException:
                statusCode = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(new
                {
                    description = alreadyExistsException.Message, // text of exception
                    entityName = alreadyExistsException.EntityName, // entity class name, that is already exists
                    existingValue = alreadyExistsException.ExistingValue // value of entity, that is already exists
                });
                break;
            
            case IncorrectPasswordException incorrectPasswordException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new
                {
                    description = incorrectPasswordException.Message, // text of exception
                    username = incorrectPasswordException.Username  // username of password owner
                });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        if (result == string.Empty)
            result = JsonSerializer.Serialize(new { errpr = exception.Message });

        return context.Response.WriteAsync(result);
    }
}