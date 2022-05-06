using Microsoft.AspNetCore.Http;

namespace Application.Documents.Services;

public interface IAppDocxService
{
    public string GetAllText(IFormFile file, string filePath);
}
