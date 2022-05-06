using Application.Rephrase;
using Application.Rephrase.DTOs;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Services.Rephrase;

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
        
        RephraseResultDTO response = _rephraseService.Rephrase(request);
        
        return Ok(response);
    }
}
