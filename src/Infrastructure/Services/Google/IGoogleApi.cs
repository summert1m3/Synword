namespace Synword.Infrastructure.Services.Google;

public interface IGoogleApi
{
    public GoogleUserModel GetGoogleUserData(string accessToken);
}
