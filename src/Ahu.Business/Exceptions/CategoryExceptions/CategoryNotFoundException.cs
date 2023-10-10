using System.Net;

namespace Ahu.Business.Exceptions.CategoryExceptions;

public class CategoryNotFoundException : Exception, IBaseException
{
    public CategoryNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public Guid Id { get; set; }
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public string ErrorMessage { get; }
}