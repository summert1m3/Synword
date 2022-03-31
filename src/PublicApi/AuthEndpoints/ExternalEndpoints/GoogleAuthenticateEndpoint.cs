using Application.Users.Commands;
using Application.Users.DTOs;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class GoogleAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GoogleAuthenticateRequest>
    .WithActionResult<UserAuthenticateDTO>
{
    private readonly IMediator _mediator;

    public GoogleAuthenticateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("api/googleAuthenticate")]
    public override async Task<ActionResult<UserAuthenticateDTO>> HandleAsync(
        GoogleAuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserAuthenticateDTO token = await _mediator.Send(
            new AuthGoogleUserCommand(request.AccessToken),
            cancellationToken);
        
        return Ok(token);
    }
}
