using System.ComponentModel.DataAnnotations;

namespace Synword.Application.AppFeatures.EnhancedRephrase.DTOs;

public class EnhancedRephraseRequestDto
{
    [Required]
    public string Text { get; set; }
}
