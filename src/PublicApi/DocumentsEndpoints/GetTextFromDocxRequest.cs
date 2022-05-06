using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.DocumentsEndpoints;

public class GetTextFromDocxRequest
{
    [Required]
    public IFormFile File { get; set; }
}
