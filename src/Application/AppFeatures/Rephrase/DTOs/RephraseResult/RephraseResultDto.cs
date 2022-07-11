namespace Application.AppFeatures.Rephrase.DTOs.RephraseResult;

public class RephraseResultDto
{
    public int Id { get; init; }
    public string SourceText { get; init; }
    public string? RephrasedText { get; init; }
    private readonly List<SourceWordSynonymsDto> _synonyms = new();
    public IList<SourceWordSynonymsDto> Synonyms => _synonyms;
}




