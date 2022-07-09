using Application.Exceptions;
using Ardalis.GuardClauses;
using MediatR;
using Synword.Domain.Entities.Identity;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Email.EmailConfirmationCodeService;

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
    private readonly IConfirmationCodeService _confirmationCodeService;
    
    public ConfirmEmailCommandHandler(
        AppIdentityDbContext identityDb,
        IConfirmationCodeService confirmationCodeService)
    {
        _identityDb = identityDb;
        _confirmationCodeService = confirmationCodeService;
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

        await _confirmationCodeService.RemoveCodeFromDb(code);
        
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

        if (code.Code.Code != request.ConfirmationCode)
        {
            throw new AppValidationException("Invalid code");
        }
    }
}
