using Application.Token.DTOs;
using Application.Token.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.AuthEndpoints.TokenEndpoints;

public class RefreshTokenEndpoint : EndpointBaseAsync
    .WithRequest<RefreshTokenRequest>
    .WithResult<ActionResult<TokenDto>>
{
    private readonly IAppTokenService _tokenService;

    public RefreshTokenEndpoint(IAppTokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("refreshToken")]
    public override async Task<ActionResult<TokenDto>> 
        HandleAsync(
            [FromForm]RefreshTokenRequest request, 
            CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        TokenDto token = await _tokenService.RefreshToken(
            request.RefreshToken, cancellationToken);
        
        return Ok(token);
    }
}
