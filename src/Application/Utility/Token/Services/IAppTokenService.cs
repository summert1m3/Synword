using Synword.Application.Utility.Token.DTOs;

namespace Synword.Application.Utility.Token.Services;

public interface IAppTokenService
{
    public Task<TokenDto> RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
