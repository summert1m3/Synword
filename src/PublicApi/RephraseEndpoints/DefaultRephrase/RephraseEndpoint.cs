using System.Security.Claims;
using Application.Rephrase;
using Application.Rephrase.DTOs;
using Application.Rephrase.DTOs.RephraseResult;
using Application.Rephrase.Services;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.RephraseEndpoints.DefaultRephrase;

public class RephraseEndpoint : EndpointBaseAsync
    .WithRequest<RephraseRequestDto>
    .WithActionResult<RephraseResultDto>
{
    private readonly IAppRephraseService _rephraseService;
    public RephraseEndpoint(IAppRephraseService rephraseService)
    {
        _rephraseService = rephraseService;
    }
    
    [HttpPost("rephrase")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "App Feature" }
    )]
    public override async Task<ActionResult<RephraseResultDto>> HandleAsync(
        [FromForm]RephraseRequestDto request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        Guard.Against.NullOrEmpty(uId, nameof(uId));
        
        RephraseResultDto response = 
            await _rephraseService.Rephrase(request, uId);
        
        return Ok(response);
    }
}
