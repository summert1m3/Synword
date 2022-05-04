using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.RephraseEndpoints;

public class RephraseEndpoint : EndpointBaseAsync
    .WithRequest<RephraseRequest>
    .WithActionResult<OkResult>
{
    public RephraseEndpoint()
    {
        
    }
    
    [HttpPost("rephrase")]
    public override async Task<ActionResult<OkResult>> HandleAsync(
        RephraseRequest request, 
        CancellationToken cancellationToken = default)
    {
        
        return Ok();
    }
}
