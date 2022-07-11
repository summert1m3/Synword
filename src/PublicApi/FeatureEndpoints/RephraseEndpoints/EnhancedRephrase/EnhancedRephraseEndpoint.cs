using System.Security.Claims;
using Application.EnhancedRephrase.DTOs;
using Application.EnhancedRephrase.Services;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Synword.Domain.Services.EnhancedRephrase;

namespace Synword.PublicApi.FeatureEndpoints.RephraseEndpoints.EnhancedRephrase;

public class EnhancedRephraseEndpoint : EndpointBaseAsync
    .WithRequest<EnhancedRephraseRequestDto>
    .WithActionResult<EnhancedRephraseResult>
{
    private readonly IAppEnhancedRephraseService _service;
    public EnhancedRephraseEndpoint(IAppEnhancedRephraseService service)
    {
        _service = service;
    }
    
    [HttpPost("enhancedRephrase")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "App Feature" }
    )]
    public override async Task<ActionResult<EnhancedRephraseResult>> 
        HandleAsync(
            [FromForm]EnhancedRephraseRequestDto request, 
            CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        Guard.Against.NullOrEmpty(uId, nameof(uId));
        
        return Ok(await _service.Rephrase(request, uId));
    }
}
