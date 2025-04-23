using System.Net;
using NasaBrowser.Domain.Common;

namespace NasaBrowser.Application.Exceptions;

public class DataSourceConvertException : ExceptionBase
{
    public DataSourceConvertException(string fieldName) : base(HttpStatusCode.InternalServerError, [$"Failed to convert the field {fieldName}"], "An error occurred while receiving data from an external source")
    {
        
    }
}