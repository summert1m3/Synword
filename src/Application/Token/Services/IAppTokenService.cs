using Application.Token.DTOs;

namespace Application.Token.Services;

public interface IAppTokenService
{
    public Task<TokenDto> RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
