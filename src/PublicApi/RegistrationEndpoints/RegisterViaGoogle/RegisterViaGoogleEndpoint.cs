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
using Synword.Persistence.Identity;

namespace Synword.PublicApi.RegistrationEndpoints.RegisterViaGoogle;

public class RegisterViaGoogleEndpoint : EndpointBaseAsync
    .WithRequest<RegisterViaGoogleRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;
    
    public RegisterViaGoogleEndpoint(
        IMediator mediator,
        UserManager<AppUser> userManager
        )
    {
        _mediator = mediator;
        _userManager = userManager;
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
        
        if (await IsUserAlreadyRegistered(userId))
        {
            throw new AppValidationException("You are already registered");
        }
        
        await _mediator.Send(
            new RegisterViaGoogleSignInCommand(request.AccessToken, userId), 
            cancellationToken);

        return Ok("Google account is linked to Synword account");
    }
    
    private async Task<bool> IsUserAlreadyRegistered(string userId)
    {
        AppUser userIdentity = await _userManager.FindByIdAsync(userId);

        var roles = await _userManager.GetRolesAsync(userIdentity);

        if (!roles.Contains(nameof(Role.Guest)))
        {
            return true;
        }

        return false;
    }
}
