namespace Application.Users.DTOs;

public class UserAuthenticateDto
{
    public UserAuthenticateDto(
        string accessToken,
        string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public string AccessToken { get; }
    public string RefreshToken { get; }
}
