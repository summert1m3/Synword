namespace Synword.Application.Guests.DTOs;

public class GuestAuthenticateDto
{
    public GuestAuthenticateDto(
        string accessToken,
        string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string AccessToken { get; }
    public string RefreshToken { get; }
}
