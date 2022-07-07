using System.Security.Claims;
using Application.Users.Commands;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synword.Domain.Enums;

namespace Synword.PublicApi.RegistrationEndpoints.ExternalEndpoints;

public class GoogleRegistrationEndpoint : EndpointBaseAsync
    .WithRequest<GoogleRegistrationRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;
    
    public GoogleRegistrationEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("googleRegister")]
    [Authorize(Roles = nameof(Role.Guest))]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]GoogleRegistrationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Guard.Against.NullOrEmpty(userId);
        
        await _mediator.Send(
            new RegisterViaGoogleSignInCommand(request.AccessToken, userId), 
            cancellationToken);

        return Ok("Google account is linked to Synword account");
    }
}
