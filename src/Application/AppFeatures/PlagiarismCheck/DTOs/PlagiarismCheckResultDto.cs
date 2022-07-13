namespace Synword.Application.AppFeatures.PlagiarismCheck.DTOs;

public class PlagiarismCheckResultDto
{
    public int Id { get; init; }
    public string Text { get; init; }
    public float Percent { get; init; }
    public List<HighlightRangeDto> Highlights { get; init; }
    public List<MatchedUrlDto> Matches { get; init; }
}
