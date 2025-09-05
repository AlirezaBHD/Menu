using System.Text.Json;
using API.Localization;
using Application.Exceptions;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse();
        int statusCode;

        switch (exception)
        {
            case ValidationException ve:
                statusCode = StatusCodes.Status400BadRequest;
                response.Message = ve.Message;
                break;
            
            case NotFoundException nfe:
                statusCode = StatusCodes.Status403Forbidden;
                response.Message = nfe.Message;
                break;
            
            case ForbiddenException fe:
                statusCode = StatusCodes.Status404NotFound;
                response.Message = fe.Message;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                response.Message = Resources.ServerError;
                break;
        }
        
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(json);
    }

    private class ErrorResponse
    {
        public string Message { get; set; } = default!;
    }
}
