using Synword.Domain.Entities.UserAggregate;

namespace Application.Validation;

public class UserValidation : IUserValidation
{
    private User? _user;
    private int _requestPrice;

    public bool IsValid(User user, int requestPrice)
    {
        _user = user;
        _requestPrice = requestPrice;

        return IsUserExist() || IsEnoughCoins();
    }

    private bool IsUserExist()
    {
        if (_user is null)
        {
            ErrorMessage = "The user does not exist";
            return false;
        }

        return true;
    }

    private bool IsEnoughCoins()
    {
        if (_user.Coins.Value < _requestPrice)
        {
            ErrorMessage = "Not enough coins to complete the operation";
            return false;
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
