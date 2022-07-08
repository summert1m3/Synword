using Application.Users.DTOs;
using Application.Users.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.AuthViaEmailEndpoints;

public class AuthViaEmailEndpoint : EndpointBaseAsync
    .WithRequest<AuthViaEmailRequest>
    .WithActionResult
{
    private readonly IUserService _userService;

    public AuthViaEmailEndpoint(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("authViaEmail")]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]AuthViaEmailRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserAuthenticateDTO token = await _userService.AuthViaEmail(
            request.Email, request.Password, cancellationToken);
        
        return Ok(token);
    }
}
