using Ardalis.GuardClauses;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Entities.RephraseAggregate;

public class RephraseResult : BaseEntity
{
    private RephraseResult()
    {
        // required by EF
    }

    public RephraseResult(
        string sourceText, string rephrasedText, List<Synonym> synonyms)
    {
        Guard.Against.NullOrEmpty(sourceText, nameof(sourceText));
        Guard.Against.NullOrEmpty(rephrasedText, nameof(rephrasedText));
        Guard.Against.Null(synonyms, nameof(synonyms));

        SourceText = sourceText;
        RephrasedText = rephrasedText;
        _synonyms = synonyms;
    }
    
    public string SourceText { get; }
    public string? RephrasedText { get; }
    private readonly List<Synonym> _synonyms;
    public IReadOnlyCollection<Synonym> Synonyms => _synonyms.AsReadOnly();
    public User User { get; private set; }
}
