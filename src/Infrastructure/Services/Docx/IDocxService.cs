using Microsoft.AspNetCore.Http;

namespace Synword.Infrastructure.Services.Docx;

public interface IDocxService
{
    public void Save(IFormFile file, string path);
    public string GetAllText(string path);
}
