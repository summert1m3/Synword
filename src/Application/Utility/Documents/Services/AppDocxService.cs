using Microsoft.AspNetCore.Http;
using Synword.Application.Interfaces.Documents;

namespace Synword.Application.Utility.Documents.Services;

public class AppDocxService : IAppDocxService
{
    private IDocxService _docxService;

    public AppDocxService(IDocxService docxService)
    {
        _docxService = docxService;
    }
    
    public string GetAllText(IFormFile file, string filePath)
    {
        _docxService.Save(file, filePath);

        string text = _docxService.GetAllText(filePath);

        return text;
    }
}
