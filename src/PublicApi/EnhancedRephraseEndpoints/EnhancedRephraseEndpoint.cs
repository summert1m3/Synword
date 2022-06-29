using System.Security.Claims;
using Application.EnhancedRephrase;
using Application.EnhancedRephrase.Services;
using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synword.Domain.Services.EnhancedRephrase;

namespace Synword.PublicApi.EnhancedRephraseEndpoints;

public class EnhancedRephraseEndpoint : EndpointBaseAsync
    .WithRequest<EnhancedRephraseRequestModel>
    .WithActionResult<EnhancedRephraseResult>
{
    private readonly IAppEnhancedRephraseService _service;
    public EnhancedRephraseEndpoint(IAppEnhancedRephraseService service)
    {
        _service = service;
    }
    
    [HttpPost("enhancedRephrase")]
    [Authorize]
    public override async Task<ActionResult<EnhancedRephraseResult>> 
        HandleAsync(
            [FromForm]EnhancedRephraseRequestModel request, 
            CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string? uId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        Guard.Against.NullOrEmpty(uId, nameof(uId));
        
        return Ok(await _service.Rephrase(request, uId));
    }
}
