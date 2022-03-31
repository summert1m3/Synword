namespace Synword.Infrastructure.Services.Google;

public interface IGoogleApi
{
    public Task<GoogleUserModel> GetGoogleUserData(string accessToken);
}
