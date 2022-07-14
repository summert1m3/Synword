using System.IdentityModel.Tokens.Jwt;
using Synword.Persistence.Entities.Identity.Token;

namespace Synword.Infrastructure.Identity.Token;

public interface IJwtService
{
    public string GenerateAccessToken(string uId);
    public RefreshToken GenerateRefreshToken(
        string uId, string deviceId);

    public void ValidateJwtToken(string token);
    public JwtSecurityToken EncodeJwtToken(string jwt);
}
