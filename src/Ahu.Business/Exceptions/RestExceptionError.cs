namespace Ahu.Business.Exceptions;

public class RestExceptionError
{
    public string Key { get; set; }
    public string Message { get; set; }

    public RestExceptionError(string key, string message)
    {
        Key = key;
        Message = message;
    }
}