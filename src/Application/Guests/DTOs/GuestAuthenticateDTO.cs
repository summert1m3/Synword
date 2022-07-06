namespace Application.Guests.DTOs;

public class GuestAuthenticateDTO
{
    public GuestAuthenticateDTO(
        string accessToken,
        string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string AccessToken { get; }
    public string RefreshToken { get; }
}
