using Ardalis.GuardClauses;
using Synword.Domain.Entities.HistoryAggregate;
using Synword.Domain.Interfaces;

namespace Synword.Domain.Entities.PlagiarismCheckAggregate;

public class PlagiarismCheckResult : BaseEntity, IAggregateRoot
{
    private PlagiarismCheckResult()
    {
        // required by EF
    }

    public PlagiarismCheckResult(
        string text, float percent, 
        List<HighlightRange> highlights, List<MatchedUrl> matches)
    {
        Guard.Against.NullOrEmpty(text, nameof(text));
        Guard.Against.OutOfRange(percent, nameof(percent), 0.0, 100.0);
        Guard.Against.Null(highlights, nameof(highlights));
        Guard.Against.Null(matches, nameof(matches));
        
        Text = text;
        Percent = percent;
        _highlights = highlights;
        _matches = matches;
    }
    
    public string? Error { get; init; }
    public string Text { get; private set; }
    public float Percent { get; private set; }
    private readonly List<HighlightRange> _highlights = new();
    public IReadOnlyCollection<HighlightRange> Highlights => _highlights.AsReadOnly();
    private readonly List<MatchedUrl> _matches = new();
    public IReadOnlyCollection<MatchedUrl> Matches => _matches.AsReadOnly();
    public History History { get; private set; } = new();
}
