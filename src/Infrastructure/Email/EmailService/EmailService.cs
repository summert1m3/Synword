using Application.Interfaces;
using Application.Interfaces.Services.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Synword.Domain.Entities.Identity;

namespace Infrastructure.Email.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfirmationCodeService _confirmationCodeService;
    private readonly IConfiguration _configuration;
    
    public EmailService(
        IConfirmationCodeService confirmationCodeService,
        IConfiguration configuration)
    {
        _confirmationCodeService = confirmationCodeService;
        _configuration = configuration;
    }
    
    public async Task SendConfirmationEmailAsync(string email)
    {
        EmailConfirmationCode code = 
            await _confirmationCodeService.CreateNew(email);
        
        var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(new MailboxAddress(
            "Synword", 
            _configuration["SmtpServiceUserName"]));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Synword - Please Verify Your Email";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = $"Confirmation code - {code.Code.Code}"
        };

        using var client = new SmtpClient();
        
        await client.ConnectAsync(
            _configuration["SmtpServiceAddress"], 
            Int32.Parse(_configuration["SmtpServicePort"]), 
            true);
        await client.AuthenticateAsync(
            _configuration["SmtpServiceUserName"], 
            _configuration["SmtpServicePassword"]);
        await client.SendAsync(emailMessage);
 
        await client.DisconnectAsync(true);
    }
}
