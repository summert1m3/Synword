using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.RephraseAggregate;

public class SourceWordSynonyms : BaseEntity
{
    private SourceWordSynonyms()
    {
        // required by EF
    }
    
    public SourceWordSynonyms(
        string sourceWord, 
        int synonymWordStartIndex, 
        int synonymWordEndIndex, 
        List<Synonym> synonyms)
    {
        Guard.Against.Null(sourceWord, nameof(sourceWord));
        Guard.Against.OutOfRange(synonymWordStartIndex, 
            nameof(synonymWordStartIndex), 0, int.MaxValue);
        Guard.Against.OutOfRange(synonymWordEndIndex, 
            nameof(synonymWordEndIndex), 0, int.MaxValue);
        Guard.Against.Negative(synonymWordStartIndex, nameof(synonymWordStartIndex));
        Guard.Against.Negative(synonymWordEndIndex, nameof(synonymWordEndIndex));
        Guard.Against.Null(synonyms, nameof(synonyms));

        SourceWord = sourceWord;
        _synonyms = synonyms;
        SynonymWordStartIndex = synonymWordStartIndex;
        SynonymWordEndIndex = synonymWordEndIndex;
    }
    
    public string SourceWord { get; private set; }
    public int SynonymWordStartIndex { get; private set; }
    public int SynonymWordEndIndex { get; private set; }
    private readonly List<Synonym> _synonyms = new();
    public IReadOnlyCollection<Synonym> Synonyms => _synonyms.AsReadOnly();
}
