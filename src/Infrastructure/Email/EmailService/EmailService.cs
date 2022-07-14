using Ardalis.GuardClauses;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Synword.Application.Exceptions;
using Synword.Application.Interfaces;
using Synword.Application.Interfaces.Email;
using Synword.Persistence.Entities.Identity;

namespace Synword.Infrastructure.Email.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfirmEmailService _confirmEmailService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<UserIdentity> _userManager;

    public EmailService(
        IConfirmEmailService confirmEmailService,
        IConfiguration configuration,
        UserManager<UserIdentity> userManager)
    {
        _confirmEmailService = confirmEmailService;
        _configuration = configuration;
        _userManager = userManager;
    }
    
    public async Task SendConfirmationEmailAsync(
        string uId, string confirmationCode)
    {
        UserIdentity identityUser = await _userManager.FindByIdAsync(uId);
        
        Guard.Against.NullOrEmpty(identityUser.Email);

        if (identityUser.EmailConfirmed)
        {
            throw new AppValidationException("Email already confirmed");
        }

        string subject = "Synword - Please Verify Your Email";
        string body = $"Confirmation code - {confirmationCode}";
        
        await SendEmailAsync(
            identityUser.Email,
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
