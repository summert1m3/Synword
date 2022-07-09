using System.Security.Claims;
using Application.Exceptions;
using Application.Users.Commands;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Infrastructure.Identity;

namespace Synword.PublicApi.UtilityEndpoints.ConfirmationEmailEndpoints;

public class ConfirmEmailEndpoint : EndpointBaseAsync
    .WithRequest<ConfirmEmailRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;
    
    public ConfirmEmailEndpoint(
        IMediator mediator,
        UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
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
        
        AppUser? identityUser = 
            await _userManager!.FindByIdAsync(userId);
        Guard.Against.Null(identityUser);

        if (identityUser.EmailConfirmed)
        {
            throw new AppValidationException("Email already confirmed");
        }

        await _mediator.Send(
            new ConfirmEmailCommand(
                request.ConfirmationCode,
                identityUser),
            cancellationToken);
        
        return Ok("Email has been successfully confirmed");
    }
}
