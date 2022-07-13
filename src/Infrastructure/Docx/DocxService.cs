using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Synword.Application.Interfaces.Services.Documents;

namespace Synword.Infrastructure.Docx;

public class DocxService : IDocxService
{
    public void Save(IFormFile file, string path)
    {
        using FileStream fileStream = File.Create(path);
        file.CopyTo(fileStream);
        fileStream.Flush();
    }
    
    public string GetAllText(string path)
    {
        string docText = "";

        using WordprocessingDocument document = 
            WordprocessingDocument.Open(path, true);
        Body body = document.MainDocumentPart!.Document.Body!;

        foreach (Text text in body.Descendants<Text>()) {
            docText += text.Text;
        }

        return docText;
    }
}
