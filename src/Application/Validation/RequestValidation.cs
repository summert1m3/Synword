using Application.Exceptions;
using Synword.Domain.Constants;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Enums;

namespace Application.Validation;

public abstract class RequestValidation
{
    protected User _user;
    protected IServiceConstraints _constraints;
    public int RequestPrice { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public abstract bool IsValid(User user, string text, int price);

    protected void SetConstraints()
    {
        foreach (var role in _user!.Roles)
        {
            _constraints = role switch
            {
                Role.Guest or Role.User => new DefaultUserServiceConstraints(),
                Role.Silver => new SilverUserServiceConstraints(),
                Role.Gold => new GoldUserServiceConstraints(),
                _ => throw new AppValidationException(
                    "The user does not have a role")
            };
        }
    }

    protected bool IsEnoughCoins(int price)
    {
        if (_user.Coins.Value < price)
        {
            ErrorMessage = "Not enough coins to complete the operation";
            return false;
        }

        return true;
    }

    protected bool MinSymbolLimitValidation(string text)
    {
        if (text.Length < DefaultUserServiceConstraints.MinSymbolLimit)
        {
            ErrorMessage = "MinSymbolLimitValidation";
            return false;
        }

        return true;
    }

    protected bool MaxSymbolLimitValidation(string text, int maxLimit)
    {
        if (text.Length > maxLimit)
        {
            ErrorMessage = "MaxSymbolLimitValidation";
            return false;
        }

        return true;
    }
}
