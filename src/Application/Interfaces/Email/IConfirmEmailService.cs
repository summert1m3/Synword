namespace Synword.Application.Interfaces.Email;

public interface IConfirmEmailService
{
    public Task<string> GenerateNewConfirmCode(string uId);
    public Task ConfirmEmail(string uId, string confirmationCode);
}
