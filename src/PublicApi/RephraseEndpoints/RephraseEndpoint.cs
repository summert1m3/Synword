using System.Security.Claims;
using Application.Rephrase;
using Application.Rephrase.DTOs;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.RephraseEndpoints;

public class RephraseEndpoint : EndpointBaseAsync
    .WithRequest<RephraseRequestModel>
    .WithActionResult<RephraseResultDTO>
{
    private readonly IAppRephraseService _rephraseService;
    public RephraseEndpoint(IAppRephraseService rephraseService)
    {
        _rephraseService = rephraseService;
    }
    
    [HttpPost("rephrase")]
    [Authorize]
    public override async Task<ActionResult<RephraseResultDTO>> HandleAsync(
        [FromForm]RephraseRequestModel request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string uId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        RephraseResultDTO response = 
            await _rephraseService.Rephrase(request, uId);
        
        return Ok(response);
    }
}
