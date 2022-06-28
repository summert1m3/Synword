namespace Application.Rephrase.DTOs;

public class RephraseResultDTO
{
    public int Id { get; init; }
    public string SourceText { get; init; }
    public string? RephrasedText { get; init; }
    private readonly List<SourceWordSynonymsDTO> _synonyms = new();
    public IList<SourceWordSynonymsDTO> Synonyms => _synonyms;
}

public class SourceWordSynonymsDTO
{
    public string SourceWord { get; init; }
    public int SynonymWordStartIndex { get; init; }
    public int SynonymWordEndIndex { get; init; }
    private readonly List<SynonymDTO> _synonyms = new();
    public IList<SynonymDTO> Synonyms => _synonyms;
}

public class SynonymDTO
{
    public string Value { get; init; }
}
