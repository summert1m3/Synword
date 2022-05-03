using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.RephraseAggregate;

public class Synonym : BaseEntity
{
    private Synonym()
    {
        // required by EF
    }
    
    public Synonym(int synonymWordStartIndex, int synonymWordEndIndex, List<string> synonyms)
    {
        Guard.Against.OutOfRange(synonymWordStartIndex, 
            nameof(synonymWordStartIndex), 0, int.MaxValue);
        Guard.Against.OutOfRange(synonymWordEndIndex, 
            nameof(synonymWordEndIndex), 0, int.MaxValue);
        Guard.Against.Negative(synonymWordStartIndex, nameof(synonymWordStartIndex));
        Guard.Against.Negative(synonymWordEndIndex, nameof(synonymWordEndIndex));
        Guard.Against.Null(synonyms, nameof(synonyms));

        _synonyms = synonyms;
        SynonymWordStartIndex = synonymWordStartIndex;
        SynonymWordEndIndex = synonymWordEndIndex;
    }
    
    public int SynonymWordStartIndex { get; private set; }
    public int SynonymWordEndIndex { get; private set; }
    private readonly List<string> _synonyms = new();
    public IReadOnlyCollection<string> Synonyms => _synonyms.AsReadOnly();
}
