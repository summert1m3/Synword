using Synword.Domain.Entities.Identity;

namespace Synword.Application.Interfaces.Services.Email;

public interface IConfirmEmailService
{
    public Task<EmailConfirmationCode> GenerateNewConfirmCode(string email);
    public Task RemoveCodeFromDb(EmailConfirmationCode code);
    public Task<bool> IsCodeValid(EmailConfirmationCode code);
}
