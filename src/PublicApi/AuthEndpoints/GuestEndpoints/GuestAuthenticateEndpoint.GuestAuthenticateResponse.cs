namespace Synword.PublicApi.AuthEndpoints.GuestEndpoints;

public class GuestAuthenticateResponse
{
    public GuestAuthenticateResponse(string token)
    {
        Token = token;
    }
    
    public string Token { get; }
}
