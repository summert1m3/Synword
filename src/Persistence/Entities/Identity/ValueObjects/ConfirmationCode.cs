using Ardalis.GuardClauses;

namespace Synword.Persistence.Entities.Identity.ValueObjects;

public class ConfirmationCode
{
    public ConfirmationCode(string code)
    {
        Guard.Against.NullOrEmpty(code);
        
        if (!code.All(char.IsDigit))
        {
            throw new Exception(
                "The confirmation code must contain only numbers");
        }
        if (code.Length > 4)
        {
            throw new Exception(
                "The confirmation code must be 4 digits long");
        }

        Code = code;
    }
    
    public string Code { get; private set; }
}
