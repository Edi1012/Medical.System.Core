using Medical.System.Core.Exceptions;
using Medical.System.Core.Messages.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Medical.System.Core.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            if (ex is AggregateException aggEx)
            {
                ex = aggEx.InnerException; // Puedes decidir cómo manejar múltiples InnerExceptions si las hay
            }
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = exception switch
        {
            NotFoundException notFoundException                             => HandleNotFoundException(notFoundException),
            ValidationException validationException                         => HandleValidationException(validationException),
            FluentValidation.ValidationException fluentValidationException  => HandleFluentValidationException(fluentValidationException),
            _ => HandleUnknownException(exception)
        };

        context.Response.StatusCode = (int)response.HttpStatusCode;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    private ErrorResponse HandleNotFoundException(NotFoundException exception)
    {
        return new ErrorResponse { HttpStatusCode = HttpStatusCode.NotFound, Message = exception.Message };
    }

    private ErrorResponse HandleValidationException(ValidationException exception)
    {
        return new ErrorResponse { HttpStatusCode = HttpStatusCode.BadRequest, Message = exception.Message };
    }

    private ErrorResponse HandleFluentValidationException(FluentValidation.ValidationException exception)
    {
        var errors = exception.Errors.Select(error => new ErrorDetail
        {
            PropertyName = error.PropertyName,
            ErrorMessage = error.ErrorMessage
        });
        return new ErrorResponse { HttpStatusCode = HttpStatusCode.BadRequest, Message = exception.Message, Errors = errors };
    }

    private ErrorResponse HandleUnknownException(Exception exception)
    {
        return new ErrorResponse { HttpStatusCode = HttpStatusCode.InternalServerError, Message = $"An unexpected error occurred: {exception.Message}." };
    }
}
//public class ExceptionMiddleware
//{
//    private readonly RequestDelegate _next;

//    public ExceptionMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task InvokeAsync(HttpContext httpContext)
//    {
//        try
//        {
//            await _next(httpContext);
//        }
//        catch (Exception ex)
//        {
//            await HandleExceptionAsync(httpContext, ex);
//        }
//    }

//    private Task HandleExceptionAsync(HttpContext context, Exception exception)
//    {

//        if (exception is AggregateException aggEx)
//        {
//            exception = aggEx.Flatten().InnerException;
//        }

//        context.Response.ContentType = "application/json";

//        int statusCode;
//        string message;
//        IEnumerable<ErrorDetail> errors = null;




//        switch (exception)
//        {
//            case NotFoundException notFoundException:
//                statusCode = (int)HttpStatusCode.NotFound;
//                message = notFoundException.Message;
//                break;
//            case ValidationException validationException:
//                statusCode = (int)HttpStatusCode.BadRequest;
//                message = validationException.Message;
//                break;
//            case FluentValidation.ValidationException FluentValidationException:
//                statusCode = (int)HttpStatusCode.BadRequest;
//                message = FluentValidationException.Message;
//                errors = FluentValidationException.Errors.Select(error => new ErrorDetail
//                {
//                    PropertyName = error.PropertyName,
//                    ErrorMessage = error.ErrorMessage
//                });
//                break;
//            default:
//                statusCode = (int)HttpStatusCode.InternalServerError;
//                message = $"An unexpected error occurred:{exception.Message}.";
//                break;
//        }

//        context.Response.StatusCode = statusCode;

//        var response = new ErrorResponse()
//        {
//            HttpStatusCode = statusCode,
//            Message = message
//        };

//        response.Errors = (exception is ValidationException) ? errors : null;

//        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
//    }
//}

