using System.Net;

namespace NasaBrowser.Domain.Common;

public abstract class ExceptionBase : Exception
{
    public HttpStatusCode StatusCode { get; }
    public IEnumerable<string> Errors { get; }

    protected ExceptionBase(HttpStatusCode statusCode, IEnumerable<string> errors, string message) : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}