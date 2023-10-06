using System.Net;

namespace Ahu.Business.Exceptions.ImagesExceptions;

public sealed class ImageNotFoundException : Exception, IBaseException
{
    public ImageNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}