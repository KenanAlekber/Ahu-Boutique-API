using System.Net;

namespace Ahu.Business.Exceptions;

public interface IBaseException
{
    Guid Id { get; }
    HttpStatusCode StatusCode { get; }
    string ErrorMessage { get; }
}