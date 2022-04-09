using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Services.PlagiarismCheck;

public class PlagiarismCheckResponseModel
{
    public string Error { get; init; }
    public string Text { get; init; }
    public float Percent { get; init; }
    public List<HighlightRange> Highlights { get; init; }
    public List<MatchedUrl> Matches { get; init; }
}
