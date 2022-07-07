using Ardalis.GuardClauses;
using Synword.Domain.Entities.Identity.ValueObjects;
using Synword.Domain.Entities.UserAggregate.ValueObjects;

namespace Synword.Domain.Entities.Identity;

public class EmailConfirmationCode : BaseEntity
{
    private EmailConfirmationCode()
    {
        // required by EF
    }
    
    public EmailConfirmationCode(ConfirmationCode code, Email email)
    {
        Guard.Against.Null(code);
        Guard.Against.Null(email);

        Code = code;
        Email = email;
    }
    
    public ConfirmationCode Code { get; private set; }
    public Email Email { get; private set; }
}
