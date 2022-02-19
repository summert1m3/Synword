namespace Synword.Infrastructure.Services.Google;

public interface IGoogleApi
{
    public bool IsAccessTokenValid(string accessToken);
    public GoogleUserModel GetGoogleUserData(string accessToken);
}
