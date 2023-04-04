using System.Net;

namespace BlazorLaboratory.WebApi.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var errorMessage = "An error occurred while processing the request.";
        var logger = context.RequestServices.GetService<ILogger>();
        logger?.LogError(ex, errorMessage);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new { message = errorMessage }.ToString()!);
    }
}
