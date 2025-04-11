using Microsoft.Extensions.Options;
using Project_Backend_2024.Facade.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Project_Backend_2024.Services.Mailing;

public class SendGridEmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<Response> SendEmailToSubject(string userEmail, string callbackUrl, string subject, string plainTextContent, string htmlContent)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(userEmail);

        var msg = MailHelper.CreateSingleEmail(new EmailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName), 
            to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response;
    }
}