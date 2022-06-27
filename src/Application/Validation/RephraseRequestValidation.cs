using Ardalis.GuardClauses;
using Synword.Domain.Entities.UserAggregate;

namespace Application.Validation;

public class RephraseRequestValidation : RequestValidation, IRephraseRequestValidation
{
    public override bool IsValid(User user, string text, int price)
    {
        Guard.Against.Null(user, nameof(user));
        _user = user;
        
        SetConstraints();

        return MinSymbolLimitValidation(text) &&
               MaxSymbolLimitValidation(text, _constraints.RephraseMaxSymbolLimit) &&
               IsEnoughCoins(price);
    }
}
