namespace OcelotApiGw.Middleware;

public static class MiddlewareExtension
{
    public static void AddMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMw>();
    }
}