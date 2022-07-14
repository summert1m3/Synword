namespace Synword.Application.Interfaces.Identity.UserIdentity;

public interface IUserRegistrationService
{
    public Task RegisterNewGuest(string uId);
    public Task RegisterGuestViaEmail(
        string uId, string email, string password);
    public Task RegisterGuestViaGoogle(string uId, string accessToken);
}
