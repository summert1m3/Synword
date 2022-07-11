using System.ComponentModel.DataAnnotations;

namespace Application.AppFeatures.EnhancedRephrase.DTOs;

public class EnhancedRephraseRequestDto
{
    [Required]
    public string Text { get; set; }
}
