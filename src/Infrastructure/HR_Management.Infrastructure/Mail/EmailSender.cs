using SendGrid;
using System.Net;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using HR_Management.Application.Models;
using HR_Management.Application.Contracts.Infrastructure;
namespace HR_Management.Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private readonly EmailSetting _emailSettings;

    public EmailSender(IOptions<EmailSetting> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmail(Email email)
    {
        SendGridClient client = new(_emailSettings.ApiKey);
        EmailAddress to = new(email.To);
        EmailAddress from = new() { Email = _emailSettings.FromAddress, Name = _emailSettings.FromName };
        SendGridMessage message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        Response response = await client.SendEmailAsync(message);
        return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
    }
}