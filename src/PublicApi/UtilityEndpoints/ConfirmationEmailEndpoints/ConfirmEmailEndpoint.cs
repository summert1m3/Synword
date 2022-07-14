using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Application.Exceptions;
using Synword.Application.Users.Commands;

namespace Synword.PublicApi.UtilityEndpoints.ConfirmationEmailEndpoints;

public class ConfirmEmailEndpoint : EndpointBaseAsync
    .WithRequest<ConfirmEmailRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;

    public ConfirmEmailEndpoint(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("confirmEmail")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "Email" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]ConfirmEmailRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guard.Against.NullOrEmpty(userId);

        await _mediator.Send(
            new ConfirmEmailCommand(
                userId,
                request.ConfirmationCode),
            cancellationToken);
        
        return Ok("Email has been successfully confirmed");
    }
}
