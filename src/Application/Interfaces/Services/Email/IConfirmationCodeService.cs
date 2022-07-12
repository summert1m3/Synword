using Synword.Domain.Entities.Identity;

namespace Application.Interfaces.Services.Email;

public interface IConfirmationCodeService
{
    public Task<EmailConfirmationCode> CreateNew(string email);
    public Task RemoveCodeFromDb(EmailConfirmationCode code);
    public Task<bool> IsCodeValid(EmailConfirmationCode code);
}
