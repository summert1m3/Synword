using Ardalis.GuardClauses;

namespace Synword.Domain.Entities.UserAggregate;

public class MatchedUrl : BaseEntity
{
    private MatchedUrl()
    {
        // required by EF
    }
    
    public MatchedUrl(string url, float percent, List<HighlightRange> highlights)
    {
        Guard.Against.Null(url, nameof(url));
        Guard.Against.OutOfRange(percent, nameof(percent), 0.0, 100.0);
        Guard.Against.Null(highlights, nameof(highlights));

        Url = url;
        Percent = percent;
        _highlights = highlights;
    }
    
    public string Url { get; private set; }
    public float Percent { get; private set; }
    private readonly List<HighlightRange> _highlights = new();
    public IReadOnlyCollection<HighlightRange> Highlights => _highlights;
}
