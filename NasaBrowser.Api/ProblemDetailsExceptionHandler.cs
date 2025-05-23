using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NasaBrowser.Domain.Common;

namespace NasaBrowser.Api;

public class ProblemDetailsExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            ExceptionBase e => (int)e.StatusCode,
            ValidationException => 400,
            _ => 500
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Title = exception switch
                    {
                        ValidationException e => e.Errors.First().ErrorCode,
                        _ => "Error occured"
                    },
                    Type = exception.GetType().Name,
                    Detail = exception switch
                    {
                        ValidationException e => e.Errors.First().ErrorMessage,
                        _ => exception.Message
                    },
                    Status = httpContext.Response.StatusCode
                }
            }
        );
    }
}