namespace Application.AppFeatures.PlagiarismCheck.DTOs;

public class MatchedUrlDto
{
    public string Url { get; private set; }
    public float Percent { get; private set; }
    private readonly List<HighlightRangeDto> _highlights = new();
    public IReadOnlyCollection<HighlightRangeDto> Highlights => _highlights;
}
