using Microsoft.Extensions.Configuration;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces.Services;

namespace Synword.Infrastructure.Services.PlagiarismCheckAPI;

public class PlagiarismCheckAPI : IPlagiarismCheckAPI
{
    private static SemaphoreSlim pool = new(5, 5);
    private readonly string _apiKey;
    private readonly string _apiUrl;
    private readonly HttpClient _httpClient = new();

    public PlagiarismCheckAPI(IConfiguration configuration)
    {
        _apiKey = configuration["PlagiarismCheckApiKey"];
        _apiUrl = configuration["PlagiarismCheckApiUrl"];
    }

    public async Task<PlagiarismCheckResult> CheckPlagiarism(string text)
    {
        pool.Wait();

        HttpResponseMessage response = await RequestAsync(text);

        pool.Release();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        string responseString = await response.Content.ReadAsStringAsync();
        PlagiarismCheckResponseModelRaw plagiarismCheckResponseModel =
            responseString.FromJson<PlagiarismCheckResponseModelRaw>();

        if (plagiarismCheckResponseModel.Error != string.Empty)
        {
            throw new Exception(plagiarismCheckResponseModel.Error);
        }

        return plagiarismCheckResponseModel.ConvertToDomainModel();
    }

    private async Task<HttpResponseMessage> RequestAsync(string text)
    {
        Dictionary<string, string> values =
            new Dictionary<string, string> {{"key", _apiKey}, {"text", text}, {"test", true.ToString()}};

        HttpResponseMessage response = await _httpClient.PostAsync(
            _apiUrl,
            new FormUrlEncodedContent(values)
        );

        return response;
    }
}

internal class PlagiarismCheckResponseModelRaw
{
    public string Error { get; set; }
    public string Text { get; set; }
    public float Percent { get; set; }
    public int[][] Highlight { get; set; }
    public MatchedUrlRaw[] Matches { get; set; }

    internal class MatchedUrlRaw
    {
        public string Url { get; set; }
        public float Percent { get; set; }
        public int[][] Highlight { get; set; }
    }

    public PlagiarismCheckResult ConvertToDomainModel()
    {
        List<HighlightRange> highlightRanges = ConvertHighlightRanges();
        List<MatchedUrl> matchedUrls = ConvertMatchedUrls();

        return new PlagiarismCheckResult(
            Text,
            Percent,
            highlightRanges,
            matchedUrls
        );
    }

    private List<HighlightRange> ConvertHighlightRanges()
    {
        List<HighlightRange> highlightRanges = new();

        foreach (var row in Highlight)
        {
            int startIndex = row[0];
            int endIndex = row[1];

            HighlightRange range = new(startIndex, endIndex);

            highlightRanges.Add(range);
        }

        return highlightRanges;
    }

    private List<MatchedUrl> ConvertMatchedUrls()
    {
        List<MatchedUrl> matchedUrls = new();

        foreach (var match in Matches)
        {
            List<HighlightRange> highlightRanges = new();
            foreach (var highlight in match.Highlight)
            {
                int startIndex = highlight[0];
                int endIndex = highlight[1];

                HighlightRange range = new(startIndex, endIndex);

                highlightRanges.Add(range);
            }

            MatchedUrl matchedUrl = new(
                match.Url, match.Percent, highlightRanges);

            matchedUrls.Add(matchedUrl);
        }

        return matchedUrls;
    }
}
