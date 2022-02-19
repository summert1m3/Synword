namespace Synword.PublicApi.RegistrationEndpoints.Guest;

public class GuestRegistrationResponse
{
    public GuestRegistrationResponse(string userId)
    {
        UserId = userId;
    }
    
    public string UserId { get; }
}
