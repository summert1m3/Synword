using Synword.Domain.Entities.RephraseAggregate;

namespace Application.Rephrase.DTOs;

public class RephraseResultDTO
{
    public string SourceText { get; init; }
    public string? RephrasedText { get; init; }
    private readonly List<SynonymDTO> _synonyms = new();
    public IList<SynonymDTO> Synonyms => _synonyms;
}

public class SynonymDTO
{
    public string SourceWord { get; init; }
    public int SynonymWordStartIndex { get; init; }
    public int SynonymWordEndIndex { get; init; }
    private readonly List<string> _synonyms = new();
    public IList<string> Synonyms => _synonyms;
}
