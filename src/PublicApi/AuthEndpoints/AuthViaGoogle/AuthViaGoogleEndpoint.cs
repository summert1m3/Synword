using Application.Users.DTOs;
using Application.Users.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.AuthEndpoints.AuthViaGoogle;

public class AuthViaGoogleEndpoint : EndpointBaseAsync
    .WithRequest<AuthViaGoogleRequest>
    .WithActionResult<UserAuthenticateDto>
{
    private readonly IUserService _userService;
    
    public AuthViaGoogleEndpoint(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("authViaGoogle")]
    [SwaggerOperation(
        Tags = new[] { "Authorization" }
    )]
    public override async Task<ActionResult<UserAuthenticateDto>> HandleAsync(
        [FromForm]AuthViaGoogleRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserAuthenticateDto token = await _userService
            .AuthViaGoogleSignIn(request.AccessToken, cancellationToken);
        
        return Ok(token);
    }
}
