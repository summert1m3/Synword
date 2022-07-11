using Synword.Domain.Entities.UserAggregate;

namespace Application.Validation.RephraseValidation;

public interface IRephraseRequestValidation
{
    public bool IsValid(User user, string text, int price);
    public string ErrorMessage { get; }
}
