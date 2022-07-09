using System.Security.Claims;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Infrastructure.Identity;

namespace Synword.PublicApi.UtilityEndpoints.UserDataEndpoints;

public class GetUserDataEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult
{
    private readonly ISynwordRepository<User> _userRepository;
    private readonly UserManager<AppUser>? _userManager;
    
    public GetUserDataEndpoint(
        ISynwordRepository<User> userRepository,
        UserManager<AppUser>? userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }
    
    [HttpGet("getUserData")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "Utility" }
    )]
    public override async Task<ActionResult> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        string? uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        AppUser userIdentity = await _userManager!.FindByIdAsync(uId);
        Guard.Against.Null(userIdentity);
        var roles = await _userManager.GetRolesAsync(userIdentity);

        User? user = await _userRepository.GetByIdAsync(uId);
        Guard.Against.Null(user);

        return Ok(
            new GetUserDataResponse(roles.First(), user.Coins.Value));
    }
}
