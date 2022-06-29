using System.ComponentModel.DataAnnotations;

namespace Application.EnhancedRephrase;

public class EnhancedRephraseRequestModel
{
    [Required]
    public string Text { get; set; }
}
