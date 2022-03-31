namespace Synword.Domain.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string id);
}
