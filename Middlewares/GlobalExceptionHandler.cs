using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementBackend.Middlewares;

public class GlobalExceptionHandler(ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;

    private readonly IWebHostEnvironment _env = env;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unexpected Error! {Message}", exception.Message);

        int statusCode = exception switch
        {
            ArgumentNullException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        bool isDevelopment = _env.IsDevelopment();
        string? errorDetail = isDevelopment ? exception.StackTrace : null;
        var errorResponse = new ProblemDetails
        {
            Detail = errorDetail,
            Status = statusCode,
            Title = "Unexpected Error"
        };

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }
}