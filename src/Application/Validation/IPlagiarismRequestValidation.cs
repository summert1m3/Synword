using Synword.Domain.Entities.UserAggregate;

namespace Application.Validation;

public interface IPlagiarismRequestValidation
{
    public bool IsValid(User user, string text, int price);
    public string ErrorMessage { get; }
}
