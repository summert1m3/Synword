using System.ComponentModel.DataAnnotations;

namespace Application.Rephrase.DTOs;

public class RephraseRequestDto
{
    [Required]
    public string Text { get; set; }
    [Required]
    public string Language { get; set; }
}
