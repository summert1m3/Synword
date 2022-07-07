using System.Security.Claims;
using Application.Users.Commands;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synword.Domain.Enums;

namespace Synword.PublicApi.RegistrationEndpoints.Email;

public class RegistrationByEmailEndpoint : EndpointBaseAsync
    .WithRequest<RegistrationByEmailRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;

    public RegistrationByEmailEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("registerViaEmail")]
    [Authorize(Roles = nameof(Role.Guest))]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]RegistrationByEmailRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Guard.Against.NullOrEmpty(userId);
        
        await _mediator.Send(
            new RegisterViaEmailCommand(
                request.Email,
                request.Password,
                userId), 
            cancellationToken);

        return Ok("Confirmation code sent to email");
    }
}
