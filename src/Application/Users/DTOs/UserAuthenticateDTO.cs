namespace Application.Users.DTOs;

public class UserAuthenticateDTO
{
    public UserAuthenticateDTO(
        string accessToken,
        string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string AccessToken { get; }
    public string RefreshToken { get; }
}
