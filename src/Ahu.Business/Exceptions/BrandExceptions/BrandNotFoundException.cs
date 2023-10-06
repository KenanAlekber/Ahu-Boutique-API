using System.Net;

namespace Ahu.Business.Exceptions.BrandExceptions;

public class BrandNotFoundException : Exception, IBaseException
{
    public BrandNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}