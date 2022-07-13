namespace Synword.Application.Interfaces.Google;

public interface IGoogleApi
{
    public Task<GoogleUserModel> GetGoogleUserData(string accessToken);
}
