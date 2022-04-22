using Application.Users.DTOs;
using Application.Users.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.ExternalEndpoints;

public class GoogleAuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<GoogleAuthenticateRequest>
    .WithActionResult<UserAuthenticateDTO>
{
    private readonly IUserService _userService;
    
    public GoogleAuthenticateEndpoint(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("googleAuthenticate")]
    public override async Task<ActionResult<UserAuthenticateDTO>> HandleAsync(
        GoogleAuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserAuthenticateDTO token = await _userService
            .Authenticate(request.AccessToken, cancellationToken);
        
        return Ok(token);
    }
}
