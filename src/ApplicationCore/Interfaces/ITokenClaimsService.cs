namespace Synword.ApplicationCore.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string id);
}
