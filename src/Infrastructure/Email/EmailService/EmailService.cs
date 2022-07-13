using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Synword.Application.Interfaces;
using Synword.Application.Interfaces.Services.Email;
using Synword.Domain.Entities.Identity;

namespace Synword.Infrastructure.Email.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfirmEmailService _confirmEmailService;
    private readonly IConfiguration _configuration;
    
    public EmailService(
        IConfirmEmailService confirmEmailService,
        IConfiguration configuration)
    {
        _confirmEmailService = confirmEmailService;
        _configuration = configuration;
    }
    
    public async Task SendConfirmationEmailAsync(string email)
    {
        EmailConfirmationCode code = 
            await _confirmEmailService.GenerateNewConfirmCode(email);

        string subject = "Synword - Please Verify Your Email";
        string body = $"Confirmation code - {code.ConfirmationCode.Code}";
        
        await SendEmailAsync(
            email,
            _configuration["SmtpServiceUserName"],
            subject,
            body
            );
    }

    private async Task SendEmailAsync(
        string to, string from, string subject, string body)
    {
        var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(new MailboxAddress(
            "Synword", 
            from));
        emailMessage.To.Add(new MailboxAddress("", to));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        using var client = new SmtpClient();
        
        await client.ConnectAsync(
            _configuration["SmtpServiceAddress"], 
            Int32.Parse(_configuration["SmtpServicePort"]), 
            true);
        await client.AuthenticateAsync(
            from, 
            _configuration["SmtpServicePassword"]);
        await client.SendAsync(emailMessage);
 
        await client.DisconnectAsync(true);
    }
}
