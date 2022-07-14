using Ardalis.GuardClauses;
using Synword.Domain.Entities;
using Synword.Domain.Entities.UserAggregate.ValueObjects;
using Synword.Persistence.Entities.Identity.ValueObjects;

namespace Synword.Persistence.Entities.Identity;

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

        ConfirmationCode = code;
        Email = email;
    }
    
    public ConfirmationCode ConfirmationCode { get; private set; }
    public Email Email { get; private set; }
}
