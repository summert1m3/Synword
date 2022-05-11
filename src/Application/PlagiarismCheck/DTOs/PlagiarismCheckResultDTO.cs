namespace Application.PlagiarismCheck.DTOs;

public class PlagiarismCheckResultDTO
{
    public string Text { get; init; }
    public float Percent { get; init; }
    public List<HighlightRangeDTO> Highlights { get; init; }
    public List<MatchedUrlDTO> Matches { get; init; }
}

public class HighlightRangeDTO
{
    public int StartIndex { get; init; }
    public int EndIndex { get; init; }
}

public class MatchedUrlDTO
{
    public string Url { get; private set; }
    public float Percent { get; private set; }
    private readonly List<HighlightRangeDTO> _highlights = new();
    public IReadOnlyCollection<HighlightRangeDTO> Highlights => _highlights;
}
