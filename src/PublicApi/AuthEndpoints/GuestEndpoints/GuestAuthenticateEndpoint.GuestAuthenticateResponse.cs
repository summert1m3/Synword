namespace Synword.PublicApi.AuthEndpoints.Guest;

public class GuestAuthenticateResponse
{
    public GuestAuthenticateResponse(string token)
    {
        Token = token;
    }
    
    public string Token { get; }
}
