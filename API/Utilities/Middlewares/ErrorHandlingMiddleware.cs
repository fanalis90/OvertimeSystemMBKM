using System.Net;
using System.Text.Json;
using API.Utilities.ViewModels;

namespace API.Utilities.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var customErrorResponse = new CustomErrorResponseVM(StatusCodes.Status500InternalServerError,
                                                                HttpStatusCode.InternalServerError.ToString(),
                                                                "Internal server error occurred. Please contact the administrator for more information.",
                                                                ex.InnerException?.Message ?? ex.Message);

            var serializedErrorResponse = JsonSerializer.Serialize(customErrorResponse);
            await context.Response.WriteAsync(serializedErrorResponse);
        }
    }
}
