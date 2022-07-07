namespace Synword.Infrastructure.Services.Email.EmailService;

public interface IEmailService
{
    public Task SendConfirmationEmailAsync(string email);
}
