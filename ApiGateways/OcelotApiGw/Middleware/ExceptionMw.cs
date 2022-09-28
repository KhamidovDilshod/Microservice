using System.Net;

namespace OcelotApiGw.Middleware;

public class ExceptionMw
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMw> _logger;

    public ExceptionMw(RequestDelegate next, ILogger<ExceptionMw> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server error ",
            ErrorMessage = $"{exception.Message}"
        }.ToString());
    }
}