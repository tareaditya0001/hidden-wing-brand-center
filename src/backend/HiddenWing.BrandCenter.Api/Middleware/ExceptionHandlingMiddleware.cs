using System.Net;
using System.Text.Json;
using FluentValidation;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.Exceptions;

namespace HiddenWing.BrandCenter.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await WriteErrorAsync(context, exception);
        }
    }

    private async Task WriteErrorAsync(HttpContext context, Exception exception)
    {
        var (statusCode, payload) = exception switch
        {
            AppException appException => (
                appException.StatusCode,
                ApiResponse.Fail(appException.Message, appException.ErrorCode)),
            ValidationException validationException => (
                (int)HttpStatusCode.BadRequest,
                ApiResponse.Fail(
                    "Validation failed.",
                    "VALIDATION_FAILED",
                    validationException.Errors.Select(error => error.ErrorMessage).Distinct().ToList())),
            _ => (
                (int)HttpStatusCode.InternalServerError,
                ApiResponse.Fail(
                    _environment.IsDevelopment() ? exception.Message : "An unexpected error occurred.",
                    "INTERNAL_ERROR"))
        };

        if (statusCode >= 500)
        {
            _logger.LogError(exception, "Unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);
        }
        else
        {
            _logger.LogWarning(exception, "Handled application exception for {Method} {Path}", context.Request.Method, context.Request.Path);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(payload, SerializerOptions));
    }
}
