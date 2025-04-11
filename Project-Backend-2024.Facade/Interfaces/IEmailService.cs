using SendGrid;

namespace Project_Backend_2024.Facade.Interfaces;

public interface IEmailService
{
    Task<Response> SendEmailToSubject(string userEmail, string callbackUrl, string subject, string plainTextContent, string htmlContent);
}