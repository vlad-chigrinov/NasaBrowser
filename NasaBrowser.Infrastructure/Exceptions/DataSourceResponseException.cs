using System.Net;
using NasaBrowser.Domain.Common;

namespace NasaBrowser.Infrastructure.Exceptions;

public class DataSourceResponseException : ExceptionBase
{
    public DataSourceResponseException(string errorMessage) : base(HttpStatusCode.InternalServerError, [errorMessage], "An error occurred while receiving data from an external resource")
    {
        
    }
}