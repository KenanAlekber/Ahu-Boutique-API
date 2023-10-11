using System.Net;

namespace Ahu.Business.Exceptions;

public class RestException : Exception
{
    public RestException(HttpStatusCode code, string errorItemKey, string errorItemMessage, string message = null)
    {
        Code = code;
        Message = message;
        Errors = new List<RestExceptionError> { new RestExceptionError(errorItemKey, errorItemMessage) };
    }

    public RestException(HttpStatusCode code, List<RestExceptionError> errors, string message = null)
    {
        Code = code;
        Message = message;
        Errors = errors;
    }

    public RestException(HttpStatusCode code, string message)
    {
        Code = code;
        Message = message;
        Errors = new List<RestExceptionError> { };
    }

    public HttpStatusCode Code { get; set; }
    public string Message { get; set; }
    public List<RestExceptionError> Errors { get; set; }
}