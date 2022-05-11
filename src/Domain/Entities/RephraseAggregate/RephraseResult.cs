using Ardalis.GuardClauses;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Entities.RephraseAggregate;

public class RephraseResult : BaseEntity
{
    private RephraseResult()
    {
        // required by EF
    }

    public RephraseResult(string rephrasedText, List<SourceWordSynonyms> synonyms)
    {
        Guard.Against.NullOrEmpty(rephrasedText, nameof(rephrasedText));
        Guard.Against.Null(synonyms, nameof(synonyms));
        
        RephrasedText = rephrasedText;
        _synonyms = synonyms;
    }
    
    public string? RephrasedText { get; private set; }

    private List<SourceWordSynonyms> _synonyms = new();
    public IReadOnlyCollection<SourceWordSynonyms> Synonyms => _synonyms.AsReadOnly();
    public User User { get; private set; }
}
