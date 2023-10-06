using System.Net;

namespace Ahu.Business.Exceptions;

public interface IBaseException
{
    HttpStatusCode StatusCode { get; }
    string ErrorMessage { get; }
}