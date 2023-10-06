using System.Net;

namespace Ahu.Business.Exceptions.ProductExceptions;

public class ProductNotFoundException  : Exception, IBaseException
{
    public ProductNotFoundException(string message)  : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}