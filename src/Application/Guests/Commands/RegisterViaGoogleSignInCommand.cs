using MediatR;
using Synword.Application.Interfaces.Identity.UserIdentity;

namespace Synword.Application.Guests.Commands;

public class RegisterViaGoogleSignInCommand : IRequest
{
    public RegisterViaGoogleSignInCommand(string accessToken, string uId)
    {
        AccessToken = accessToken;
        UId = uId;
    }
    
    public string AccessToken { get; }
    public string UId { get; }
}

internal class RegisterViaGoogleSignInCommandHandler : 
    IRequestHandler<RegisterViaGoogleSignInCommand>
{
    private readonly IUserRegistrationService _registration;

    public RegisterViaGoogleSignInCommandHandler(
        IUserRegistrationService registration)
    {
        _registration = registration;
    }
    
    public async Task<Unit> Handle(
        RegisterViaGoogleSignInCommand request, 
        CancellationToken cancellationToken)
    {
        await _registration.RegisterGuestViaGoogle(
            request.UId, request.AccessToken);
        
        return Unit.Value;
    }
}
