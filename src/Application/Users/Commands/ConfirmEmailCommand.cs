using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Synword.Domain.Entities.Identity;
using Synword.Infrastructure.Identity;

namespace Application.Users.Commands;

public class ConfirmEmailCommand : IRequest
{
    public ConfirmEmailCommand(
        string confirmationCode,
        string uId)
    {
        ConfirmationCode = confirmationCode;
        UId = uId;
    }
    
    public string ConfirmationCode { get; }
    public string UId { get; }
}

internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppIdentityDbContext _identityDb;
    
    public ConfirmEmailCommandHandler(
        UserManager<AppUser> userManager,
        AppIdentityDbContext identityDb)
    {
        _userManager = userManager;
        _identityDb = identityDb;
    }
    
    public async Task<Unit> Handle(
        ConfirmEmailCommand request, 
        CancellationToken cancellationToken)
    {
        AppUser userIdentity = await _userManager.FindByIdAsync(request.UId);

        EmailConfirmationCode? code = _identityDb.EmailConfirmationCodes
                .FirstOrDefault(
                    e => e.Email.Value == userIdentity.Email);
        
        Validation(code, request);

        userIdentity.EmailConfirmed = true;

        await RemoveCodeFromDb(code!, cancellationToken);
        
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

    private async Task RemoveCodeFromDb(
        EmailConfirmationCode code,
        CancellationToken cancellationToken)
    {
        _identityDb.EmailConfirmationCodes.Remove(code!);
        await _identityDb.SaveChangesAsync(cancellationToken);
    }
}
