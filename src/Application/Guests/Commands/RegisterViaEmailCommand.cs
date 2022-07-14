using MediatR;
using Synword.Application.Interfaces.Identity.UserIdentity;

namespace Synword.Application.Guests.Commands;

public class RegisterViaEmailCommand : IRequest
{
    public RegisterViaEmailCommand(
        string email,
        string password,
        string uId)
    {
        Email = email;
        Password = password;
        UId = uId;
    }
    
    public string Email { get; }
    public string Password { get; }
    public string UId { get; }
}

internal class RegisterViaEmailCommandHandler 
    : IRequestHandler<RegisterViaEmailCommand>
{
    private readonly IUserRegistrationService _registration;

    public RegisterViaEmailCommandHandler(
        IUserRegistrationService registration)
    {
        _registration = registration;
    }

    public async Task<Unit> Handle(
        RegisterViaEmailCommand request, 
        CancellationToken cancellationToken)
    {
        await _registration.RegisterGuestViaEmail(
            request.UId, request.Email, request.Password);

        return Unit.Value;
    }
}
