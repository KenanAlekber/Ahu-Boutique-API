using System.Net;

namespace Ahu.Business.Exceptions.SliderExceptions;

public class SliderNotFoundException : Exception, IBaseException
{
    public SliderNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public Guid Id { get; set; }
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}