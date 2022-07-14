using MediatR;
using Synword.Application.Interfaces;
using Synword.Application.Interfaces.Email;

namespace Synword.Application.Users.Commands;

public class SendConfirmEmailCommand : IRequest
{
    public SendConfirmEmailCommand(string uId)
    {
        UId = uId;
    }
    
    public string UId { get; }
}

internal class SendConfirmEmailCommandHandler : IRequestHandler<SendConfirmEmailCommand>
{
    private readonly IEmailService _emailService;
    private readonly IConfirmEmailService _confirmEmailService;

    public SendConfirmEmailCommandHandler(
        IEmailService emailService,
        IConfirmEmailService confirmEmailService)
    {
        _emailService = emailService;
        _confirmEmailService = confirmEmailService;
    }
    
    public async Task<Unit> Handle(
        SendConfirmEmailCommand request, 
        CancellationToken cancellationToken)
    {
        string confirmationCode = 
            await _confirmEmailService.GenerateNewConfirmCode(request.UId);
        await _emailService.SendConfirmationEmailAsync(
            request.UId, confirmationCode);
        
        return Unit.Value;
    }
}
