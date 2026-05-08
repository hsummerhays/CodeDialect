using CodeDialect.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CodeDialect.WebAPI.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            ValidationException ve => (
                StatusCodes.Status400BadRequest,
                "Validation failed",
                string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))),

            NotFoundException => (
                StatusCodes.Status404NotFound,
                "Not found",
                exception.Message),

            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                exception.Message),

            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred",
                "An internal server error occurred.")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception");

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        }, cancellationToken);

        return true;
    }
}
