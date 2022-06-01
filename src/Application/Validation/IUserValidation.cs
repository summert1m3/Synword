using Synword.Domain.Entities.UserAggregate;

namespace Application.Validation;

public interface IUserValidation
{
    public bool IsValid(User user, int requestPrice);
    public string ErrorMessage { get; }
}
