using System.Net;
using NasaBrowser.Domain.Common;

namespace NasaBrowser.Application.Exceptions;

public class ValidationException : ExceptionBase
{
    public ValidationException(IEnumerable<string> errors) : base(HttpStatusCode.BadRequest, errors, "Validation exception")
    {
    }
}