using System.Security.Claims;
using Application.Users.Commands;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.RegistrationEndpoints.Email;

public class ConfirmEmailEndpoint : EndpointBaseAsync
    .WithRequest<ConfirmEmailRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;
    
    public ConfirmEmailEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("confirmEmail")]
    [Authorize]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]ConfirmEmailRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Guard.Against.NullOrEmpty(userId);
        
        await _mediator.Send(
            new ConfirmEmailCommand(
                request.ConfirmationCode,
                userId),
            cancellationToken);
        
        return Ok("Email has been successfully confirmed");
    }
}
