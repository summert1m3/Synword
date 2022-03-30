namespace Synword.PublicApi.RegistrationEndpoints.GuestEndpoints;

public class GuestRegistrationResponse
{
    public GuestRegistrationResponse(string userId)
    {
        UserId = userId;
    }
    
    public string UserId { get; }
}
