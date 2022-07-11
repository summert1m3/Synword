using System.ComponentModel.DataAnnotations;

namespace Application.EnhancedRephrase.DTOs;

public class EnhancedRephraseRequestDto
{
    [Required]
    public string Text { get; set; }
}
