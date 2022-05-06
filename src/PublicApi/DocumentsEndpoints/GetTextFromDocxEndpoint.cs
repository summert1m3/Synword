using System.Security.Claims;
using Application.Documents.Services;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Synword.PublicApi.DocumentsEndpoints;

public class GetTextFromDocxEndpoint : EndpointBaseAsync
    .WithRequest<GetTextFromDocxRequest>
    .WithActionResult<GetTextFromDocxResponse>
{
    private readonly IWebHostEnvironment _env;
    private readonly IAppDocxService _docxService;
    private static long _fileSaveCount = 0;
    
    public GetTextFromDocxEndpoint(
        IWebHostEnvironment env, 
        IAppDocxService docxService)
    {
        _env = env;
        _docxService = docxService;
    }
    
    [HttpPost("getTextFromDocx")]
    [Authorize]
    public override async Task<ActionResult<GetTextFromDocxResponse>>
        HandleAsync(
            [FromForm] GetTextFromDocxRequest request,
            CancellationToken cancellationToken = default
        )
    {
        Interlocked.Increment(ref _fileSaveCount);

        string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        string path = _env.ContentRootPath + @"/Documents/temp/docx/";
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        string filePath = 
            path + userId + "_" + _fileSaveCount + "_" + request.File.FileName;
        
        string text = _docxService.GetAllText(request.File, filePath);

        GetTextFromDocxResponse response = new() {Text = text};

        return Ok(response);
    }
}
