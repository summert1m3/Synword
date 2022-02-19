namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class GoogleAuthenticateResponse
{
    public GoogleAuthenticateResponse(string token)
    {
        Token = token;
    }
    
    public string Token { get; }
}
