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

namespace Synword.PublicApi.RegistrationEndpoints.RegisterViaEmail;

public class RegistrationByEmailEndpoint : EndpointBaseAsync
    .WithRequest<RegistrationByEmailRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;

    public RegistrationByEmailEndpoint(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("registerViaEmail")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "Registration" }
    )]
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

        return Ok("The mail has been successfully attached to the account");
    }
}
