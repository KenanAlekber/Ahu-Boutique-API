using System.Net;

namespace Ahu.Business.Exceptions.OrderExceptions;

public class OrderNotFoundException : Exception, IBaseException
{
    public OrderNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public Guid Id { get; set; }
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}