using Ardalis.GuardClauses;
using Synword.Domain.Entities.HistoryAggregate;

namespace Synword.Domain.Entities.RephraseAggregate;

public class RephraseResult : BaseEntity
{
    private RephraseResult()
    {
        // required by EF
    }

    public RephraseResult(
        string sourceText, 
        string rephrasedText, 
        List<SourceWordSynonyms> synonyms)
    {
        Guard.Against.NullOrEmpty(sourceText, nameof(sourceText));
        Guard.Against.NullOrEmpty(rephrasedText, nameof(rephrasedText));
        Guard.Against.Null(synonyms, nameof(synonyms));

        SourceText = sourceText;
        RephrasedText = rephrasedText;
        _synonyms = synonyms;
    }
    public string SourceText { get; private set; }
    public string? RephrasedText { get; private set; }

    private List<SourceWordSynonyms> _synonyms = new();
    public IReadOnlyCollection<SourceWordSynonyms> Synonyms => _synonyms.AsReadOnly();
    public History History { get; private set; } = new();
}
