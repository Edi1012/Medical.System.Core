using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Medical.System.Core.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ErrorResponse
        {
            Message = "Ocurrió un error."
        };

        if (exception is ValidationException validationException)
        {
            response.Errors = validationException.Errors.Select(error => new ErrorDetail
            {
                PropertyName = error.PropertyName,
                ErrorMessage = error.ErrorMessage
            });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest; // o cualquier otro código de estado según la excepción
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}

public class ErrorResponse
{
    public string Message { get; set; }
    public IEnumerable<ErrorDetail> Errors { get; set; }
}

public class ErrorDetail
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}