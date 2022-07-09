using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.UtilityEndpoints.DocumentsEndpoints;

public class GetTextFromDocxRequest
{
    [Required]
    public IFormFile File { get; set; }
}
