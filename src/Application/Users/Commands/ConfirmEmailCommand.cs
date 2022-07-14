using MediatR;
using Synword.Application.Interfaces.Email;

namespace Synword.Application.Users.Commands;

public class ConfirmEmailCommand : IRequest
{
    public ConfirmEmailCommand(
        string uId,
        string confirmationCode)
    {
        UId = uId;
        ConfirmationCode = confirmationCode;
    }
    
    public string UId { get; }
    public string ConfirmationCode { get; }
}

internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IConfirmEmailService _confirmEmailService;
    
    public ConfirmEmailCommandHandler(
        IConfirmEmailService confirmEmailService)
    {
        _confirmEmailService = confirmEmailService;
    }
    
    public async Task<Unit> Handle(
        ConfirmEmailCommand request, 
        CancellationToken cancellationToken)
    {
        await _confirmEmailService.ConfirmEmail(request.UId, request.ConfirmationCode);

        return Unit.Value;
    }
}
