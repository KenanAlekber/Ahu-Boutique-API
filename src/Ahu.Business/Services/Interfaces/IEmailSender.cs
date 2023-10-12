namespace Ahu.Business.Services.Interfaces;

public interface IEmailSender
{
    void Send(string to, string subject, string text);
}