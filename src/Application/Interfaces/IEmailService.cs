namespace Application.Interfaces;

public interface IEmailService
{
    public Task SendConfirmationEmailAsync(string email);
}
