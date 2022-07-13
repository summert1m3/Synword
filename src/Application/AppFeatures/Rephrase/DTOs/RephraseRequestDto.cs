using System.ComponentModel.DataAnnotations;

namespace Synword.Application.AppFeatures.Rephrase.DTOs;

public class RephraseRequestDto
{
    [Required]
    public string Text { get; set; }
    [Required]
    public string Language { get; set; }
}
