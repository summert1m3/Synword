using System.Security.Claims;
using Application.PlagiarismCheck.DTOs;
using Application.PlagiarismCheck.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Synword.PublicApi.PlagiarismCheckEndpoints;

public class PlagiarismCheckEndpoint : EndpointBaseAsync
    .WithRequest<PlagiarismCheckRequest>
    .WithActionResult<PlagiarismCheckResultDTO>
{
    private readonly IAppPlagiarismCheckService _plagiarismCheck;
    
    public PlagiarismCheckEndpoint(IAppPlagiarismCheckService plagiarismCheck)
    {
        _plagiarismCheck = plagiarismCheck;
    }
    
    [HttpPost("plagiarismCheck")]
    [Authorize]
    [SwaggerOperation(
        Tags = new[] { "App Feature" }
    )]
    public override async Task<ActionResult<PlagiarismCheckResultDTO>> HandleAsync(
        [FromForm]PlagiarismCheckRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        string uId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        PlagiarismCheckResultDTO response 
            = await _plagiarismCheck.CheckPlagiarism(request.Text, uId);
        
        return Ok(response);
    }
}
