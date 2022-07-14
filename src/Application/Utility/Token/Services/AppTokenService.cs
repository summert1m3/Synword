using Synword.Application.Interfaces.Identity.Token;
using Synword.Application.Utility.Token.DTOs;

namespace Synword.Application.Utility.Token.Services;

public class AppTokenService : IAppTokenService
{
    private readonly IRefreshTokenService _tokenService;

    public AppTokenService(
        IRefreshTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TokenDto> RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        return await _tokenService.RefreshToken(refreshToken, cancellationToken);
    }
}
