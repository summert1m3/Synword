using Synword.Domain.Entities.Identity.Token;

namespace Application.Interfaces.Services.Token;

public interface ITokenClaimsService
{
    public string GenerateAccessToken(string uId);
    public RefreshToken GenerateRefreshToken(
        string uId, string deviceId);

    public void ValidateJwtToken(string token);
}
