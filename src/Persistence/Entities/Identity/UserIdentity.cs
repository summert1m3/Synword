using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Synword.Persistence.Entities.Identity.Token;
using Synword.Persistence.Entities.Identity.ValueObjects;

namespace Synword.Persistence.Entities.Identity;

public class UserIdentity : IdentityUser
{
    private List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens 
        => _refreshTokens.AsReadOnly();
    public ExternalSignIn? ExternalSignIn { get; private set; }

    public void AddRefreshToken(RefreshToken token)
    {
        _refreshTokens.Add(token);
    }
    
    public void AddExternalSignIn(ExternalSignIn externalSignIn)
    {
        Guard.Against.Null(externalSignIn, nameof(externalSignIn));

        if (ExternalSignIn != null)
        {
            throw new Exception("ExternalSignIn already exist");
        }

        ExternalSignIn = externalSignIn;
    }
}
