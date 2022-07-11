using Application.Users.DTOs;
using Application.Users.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(
        Tags = new[] { "Authorization" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromForm]AuthViaEmailRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        UserAuthenticateDto token = await _userService.AuthViaEmail(
            request.Email, request.Password, cancellationToken);
        
        return Ok(token);
    }
}
