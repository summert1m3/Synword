using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.Identity.Token;

namespace Synword.Infrastructure.Identity;

public class AppUser : IdentityUser
{
    private List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens 
        => _refreshTokens.AsReadOnly();

    public void AddRefreshToken(RefreshToken token)
    {
        _refreshTokens.Add(token);
    }
}
