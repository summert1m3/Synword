using System.ComponentModel.DataAnnotations;

namespace Synword.PublicApi.FeatureEndpoints.PlagiarismCheckEndpoints;

public class PlagiarismCheckRequest
{
    [Required]
    public string Text { get; set; }
}
