using Application.PlagiarismCheck.DTOs;
using Application.PlagiarismCheck.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.PlagiarismCheckEndpoints;

public class PlagiarismCheckEndpoint : EndpointBaseAsync
    .WithRequest<PlagiarismCheckRequest>
    .WithActionResult<PlagiarismCheckResponseDTO>
{
    private readonly IAppPlagiarismCheckService _plagiarismCheck;
    
    public PlagiarismCheckEndpoint(IAppPlagiarismCheckService plagiarismCheck)
    {
        _plagiarismCheck = plagiarismCheck;
    }
    
    [HttpPost("plagiarismCheck")]
    //[Authorize]
    public override async Task<ActionResult<PlagiarismCheckResponseDTO>> HandleAsync(
        [FromForm]PlagiarismCheckRequest request, 
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        PlagiarismCheckResponseDTO response 
            = await _plagiarismCheck.CheckPlagiarism(request.Text);
        
        return Ok(response);
    }
}
