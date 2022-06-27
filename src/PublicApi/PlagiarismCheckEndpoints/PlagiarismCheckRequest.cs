using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.PlagiarismCheckEndpoints;

public class PlagiarismCheckRequest
{
    [Required]
    public string Text { get; set; }
}
