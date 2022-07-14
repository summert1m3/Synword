using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Application.Exceptions;
using Synword.Application.Guests.Commands;
using Synword.Domain.Enums;

namespace Synword.PublicApi.RegistrationEndpoints.RegisterViaGoogle;

public class RegisterViaGoogleEndpoint : EndpointBaseAsync
    .WithRequest<RegisterViaGoogleRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;

    public RegisterViaGoogleEndpoint(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("registerViaGoogle")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "Registration" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]RegisterViaGoogleRequest request,
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
