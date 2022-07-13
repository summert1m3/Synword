namespace Synword.Application.AppFeatures.Rephrase.DTOs.RephraseResult;

public class SourceWordSynonymsDto
{
    public string SourceWord { get; init; }
    public int SynonymWordStartIndex { get; init; }
    public int SynonymWordEndIndex { get; init; }
    private readonly List<SynonymDto> _synonyms = new();
    public IList<SynonymDto> Synonyms => _synonyms;
}
