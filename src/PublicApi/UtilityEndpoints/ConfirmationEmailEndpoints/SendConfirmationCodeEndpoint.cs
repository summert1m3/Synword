using System.Security.Claims;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Application.Users.Commands;

namespace Synword.PublicApi.UtilityEndpoints.ConfirmationEmailEndpoints;

public class SendConfirmationCodeEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult
{
    private readonly IMediator _mediator;

    public SendConfirmationCodeEndpoint(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("sendConfirmationCode")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "Email" }
    )]
    public override async Task<ActionResult> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _mediator.Send(
            new SendConfirmEmailCommand(userId),
            cancellationToken);
        
        return Ok("Confirmation code sent to email");
    }
}
