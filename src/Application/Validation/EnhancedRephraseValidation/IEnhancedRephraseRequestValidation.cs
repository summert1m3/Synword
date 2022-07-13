using Synword.Domain.Entities.UserAggregate;

namespace Synword.Application.Validation.EnhancedRephraseValidation;

public interface IEnhancedRephraseRequestValidation
{
    public bool IsValid(User user, string text, int price);
    public string ErrorMessage { get; }
}
