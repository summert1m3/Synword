using Synword.Domain.Entities.Identity;
using Synword.Domain.Entities.Identity.ValueObjects;

namespace Synword.Infrastructure.Services.Email.EmailConfirmationCodeService;

public interface IConfirmationCodeService
{
    public Task<EmailConfirmationCode> CreateNew(string email);
    public Task RemoveCodeFromDb(EmailConfirmationCode code);
    public Task<bool> IsCodeValid(EmailConfirmationCode code);
}
