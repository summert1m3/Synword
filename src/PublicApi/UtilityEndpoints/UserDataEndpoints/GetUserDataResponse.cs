namespace Synword.PublicApi.UtilityEndpoints.UserDataEndpoints;

public class GetUserDataResponse
{
    public GetUserDataResponse(string role, int balance)
    {
        Role = role;
        Balance = balance;
    }
    
    public string Role { get; }
    public int Balance { get; }
}
