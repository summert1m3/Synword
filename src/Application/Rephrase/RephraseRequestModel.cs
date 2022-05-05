using System.ComponentModel.DataAnnotations;

namespace Application.Rephrase;

public class RephraseRequestModel
{
    [Required]
    public string Text { get; set; }
    [Required]
    public string Language { get; set; }
}
