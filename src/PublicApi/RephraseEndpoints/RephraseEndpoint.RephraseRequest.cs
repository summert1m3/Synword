using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.RephraseEndpoints;

public class RephraseRequest
{
    [Required]
    public string Text { get; set; }
    [Required]
    public string Language { get; set; }
}
