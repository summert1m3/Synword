namespace Synword.Infrastructure.Services.Google;

public class GoogleApi : IGoogleApi
{
    public bool IsAccessTokenValid(string accessToken)
    {
        return true;
    }

    public GoogleUserModel GetGoogleUserData(string accessToken)
    {
        GoogleUserModel userData = new();
        return userData;
    }
}
