using System.Security.Claims;
using Application.Exceptions;
using Application.Guests.Commands;
using Application.Users.Commands;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Synword.Domain.Enums;
using Synword.Infrastructure.Identity;

namespace Synword.PublicApi.RegistrationEndpoints.Email;

public class RegistrationByEmailEndpoint : EndpointBaseAsync
    .WithRequest<RegistrationByEmailRequest>
    .WithActionResult
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public RegistrationByEmailEndpoint(
        IMediator mediator,
        UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }
    
    [HttpPost("registerViaEmail")]
    [Authorize]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]RegistrationByEmailRequest request, 
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
            new RegisterViaEmailCommand(
                request.Email,
                request.Password,
                userId), 
            cancellationToken);

        return Ok("Confirmation code sent to email");
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
