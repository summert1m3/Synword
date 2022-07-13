using Microsoft.AspNetCore.Http;

namespace Synword.Application.Utility.Documents.Services;

public interface IAppDocxService
{
    public string GetAllText(IFormFile file, string filePath);
}
