using Synword.Application.Utility.Token.DTOs;

namespace Synword.Application.Interfaces.Identity.Token;

public interface IRefreshTokenService
{
    public Task<TokenDto> RefreshToken(
        string inputRefreshToken,
        CancellationToken cancellationToken = default);
}
