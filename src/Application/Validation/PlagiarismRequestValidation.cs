using Ardalis.GuardClauses;
using Synword.Domain.Constants;
using Synword.Domain.Entities.UserAggregate;

namespace Application.Validation;

public class PlagiarismRequestValidation : RequestValidation, IPlagiarismRequestValidation
{
    public override bool IsValid(User user, string text, int price)
    {
        Guard.Against.Null(user, nameof(user));
        _user = user;
        
        SetConstraints();

        return MinSymbolLimitValidation(text) &&
               MaxSymbolLimitValidation(text, _constraints.PlagiarismCheckMaxSymbolLimit) &&
               IsEnoughCoins(price);
    }
}
