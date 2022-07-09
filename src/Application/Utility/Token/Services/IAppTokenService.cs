using Application.Utility.Token.DTOs;

namespace Application.Utility.Token.Services;

public interface IAppTokenService
{
    public Task<TokenDto> RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
