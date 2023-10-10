using System.Net;

namespace Ahu.Business.Exceptions.BrandExceptions;

public class BrandNotFoundException : Exception, IBaseException
{
    public BrandNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public Guid Id { get; set; }
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}