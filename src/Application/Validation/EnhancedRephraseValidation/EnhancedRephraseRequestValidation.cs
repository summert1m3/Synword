using Ardalis.GuardClauses;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Application.Validation.EnhancedRephraseValidation;

public class EnhancedRephraseRequestValidation : RequestValidation, IEnhancedRephraseRequestValidation
{
    public override bool IsValid(User user, string text, int price)
    {
        Guard.Against.Null(user, nameof(user));
        _user = user;
        
        SetConstraints();

        return MinSymbolLimitValidation(text) &&
               MaxSymbolLimitValidation(
                   text,
                   _constraints.EnhancedRephraseMaxSymbolLimit) &&
               IsEnoughCoins(price);
    }

    public new string ErrorMessage { get; }
}
