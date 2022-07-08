using Application.Users.DTOs;
using Application.Users.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.AuthViaGoogle;

public class AuthViaGoogleEndpoint : EndpointBaseAsync
    .WithRequest<AuthViaGoogleRequest>
    .WithActionResult<UserAuthenticateDTO>
{
    private readonly IUserService _userService;
    
    public AuthViaGoogleEndpoint(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("authViaGoogle")]
    public override async Task<ActionResult<UserAuthenticateDTO>> HandleAsync(
        [FromForm]AuthViaGoogleRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserAuthenticateDTO token = await _userService
            .AuthViaGoogleSignIn(request.AccessToken, cancellationToken);
        
        return Ok(token);
    }
}
