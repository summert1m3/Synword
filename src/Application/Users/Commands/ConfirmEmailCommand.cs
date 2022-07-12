using Application.Exceptions;
using Application.Interfaces.Services.Email;
using Ardalis.GuardClauses;
using MediatR;
using Synword.Domain.Entities.Identity;
using Synword.Infrastructure.Identity;

namespace Application.Users.Commands;

public class ConfirmEmailCommand : IRequest
{
    public ConfirmEmailCommand(
        string confirmationCode,
        AppUser identityUser)
    {
        Guard.Against.Null(identityUser);
        
        ConfirmationCode = confirmationCode;
        IdentityUser = identityUser;
    }
    
    public string ConfirmationCode { get; }
    public AppUser IdentityUser { get; }
}

internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly AppIdentityDbContext _identityDb;
    private readonly IConfirmEmailService _confirmEmailService;
    
    public ConfirmEmailCommandHandler(
        AppIdentityDbContext identityDb,
        IConfirmEmailService confirmEmailService)
    {
        _identityDb = identityDb;
        _confirmEmailService = confirmEmailService;
    }
    
    public async Task<Unit> Handle(
        ConfirmEmailCommand request, 
        CancellationToken cancellationToken)
    {
        AppUser userIdentity = request.IdentityUser;

        EmailConfirmationCode? code = _identityDb.EmailConfirmationCodes
                .FirstOrDefault(
                    e => e.Email.Value == userIdentity.Email);
        
        Validation(code, request);

        userIdentity.EmailConfirmed = true;

        await _confirmEmailService.RemoveCodeFromDb(code);
        
        return Unit.Value;
    }

    private void Validation(
        EmailConfirmationCode? code,
        ConfirmEmailCommand request)
    {
        if (code is null)
        {
            throw new AppValidationException("Code not found");
        }

        if (code.ConfirmationCode.Code != request.ConfirmationCode)
        {
            throw new AppValidationException("Invalid code");
        }
    }
}
