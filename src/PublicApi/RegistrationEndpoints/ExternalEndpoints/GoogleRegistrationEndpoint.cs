using Application.Users.Commands;
using Ardalis.ApiEndpoints;
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
        
        await _mediator.Send(
            new RegisterNewGoogleUserCommand(request.AccessToken, User), 
            cancellationToken);

        return Ok("Google account is linked to Synword account");
    }
}
