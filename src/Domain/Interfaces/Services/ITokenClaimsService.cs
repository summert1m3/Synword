namespace Synword.Domain.Interfaces.Services;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string id);
}
