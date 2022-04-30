using Microsoft.Extensions.Logging;
using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces;

namespace Synword.Domain.Services.PlagiarismCheck;

public class PlagiarismCheckService : IPlagiarismCheckService
{
    private readonly IPlagiarismCheckAPI _plagiarismCheckApi;
    private readonly ILogger<PlagiarismCheckService> _logger;
    private const int ApiInputRestriction = 20000;

    public PlagiarismCheckService(IPlagiarismCheckAPI plagiarismCheckApi, 
        ILogger<PlagiarismCheckService> logger)
    {
        _plagiarismCheckApi = plagiarismCheckApi;
        _logger = logger;
    }

    public async Task<PlagiarismCheckResponseModel> CheckPlagiarism(string text)
    {
        PlagiarismCheckResponseModel model = new();
        
        if (text.Length <= ApiInputRestriction)
        {
            model = await CheckPlagiarismUnderLimit(text);
        }
        else if (text.Length > ApiInputRestriction)
        {
            model = await CheckPlagiarismOverLimit(text);
        }

        return model;
    }
    
    private async Task<PlagiarismCheckResponseModel> CheckPlagiarismUnderLimit(string text)
    {
        return await _plagiarismCheckApi.CheckPlagiarism(text);
    }

    private async Task<PlagiarismCheckResponseModel> CheckPlagiarismOverLimit(string text)
    {
        List<string> splitText = GetSplitText(text);

        List<PlagiarismCheckResponseModel> splitUniqueCheckResponse =
            await CheckPlagiarismForSplitText(splitText);

        float plagiarismPercent = PercentCorrection(
            splitUniqueCheckResponse, splitText);
        
        string allText = MergeSplitText(splitUniqueCheckResponse);

        List<int> wordCountsSums = 
            WordCountsNextElIsSumOfPrevious(splitUniqueCheckResponse);
        
        List<HighlightRange> highlights = MergeHighlights(
            splitUniqueCheckResponse,
            wordCountsSums);

        List<MatchedUrl> matchedUrls = MergeMatchedUrls(
            splitUniqueCheckResponse,
            wordCountsSums);

        PlagiarismCheckResponseModel model = new()
        {
            Error = string.Empty,
            Highlights = highlights,
            Matches = matchedUrls,
            Percent = (float)Math.Round(plagiarismPercent, 1),
            Text = allText
        };

        return model;
    }

    private async Task<List<PlagiarismCheckResponseModel>> CheckPlagiarismForSplitText(
        IEnumerable<string> splitText)
    {
        List<PlagiarismCheckResponseModel> splitUniqueCheckResponse = new();
        
        List<Task<PlagiarismCheckResponseModel>> tasks = new();
        
        foreach (var text in splitText)
        {
            tasks.Add(_plagiarismCheckApi.CheckPlagiarism(text));
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            splitUniqueCheckResponse.Add(task.Result);
        }

        return splitUniqueCheckResponse;
    }

    private float PercentCorrection(
        IReadOnlyList<PlagiarismCheckResponseModel> splitUniqueCheckResponse,
        IReadOnlyList<string> splitText)
    {
        double percentRatioFromOtherParts =
            CalculatePercentLastElementFromOtherParts(splitText);

        double sum = SumOfPercentWithoutLastElement(
            splitUniqueCheckResponse);

        double average = ArithmeticMean(sum, splitUniqueCheckResponse);
        
        double correctionPercent = LastElementPercentCorrection(
            average,
            splitUniqueCheckResponse.Last().Percent,
            percentRatioFromOtherParts);

        float plagiarismPercent = (float)ApplyCorrectionOfLastElement(
            correctionPercent, average, splitUniqueCheckResponse.Last().Percent);

        return plagiarismPercent;
    }

    private double CalculatePercentLastElementFromOtherParts(IReadOnlyList<string> splitText)
    {
        int sumOfOtherParts = 0;
        for (int i = 0; i < splitText.Count - 1; i++)
        {
            sumOfOtherParts += splitText[i].Length;
        }

        double percentRatioFromOtherParts =
            (splitText.Last().Length * 100.0)
            / sumOfOtherParts;

        return percentRatioFromOtherParts;
    }

    private double LastElementPercentCorrection(
        double firstPartPercent,
        double secondPartPercent,
        double percentRatioFromOtherParts)
    {
        double difference;

        if (firstPartPercent > secondPartPercent)
        {
            difference = firstPartPercent - secondPartPercent;
        }
        else if (firstPartPercent < secondPartPercent)
        {
            difference = secondPartPercent - firstPartPercent;
        }
        else
        {
            difference = 0;
        }

        double correction = (difference / 100.0) * percentRatioFromOtherParts;

        return correction;
    }

    private double SumOfPercentWithoutLastElement(
        IReadOnlyList<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        double sum = 0;

        for (int i = 0; i < (splitUniqueCheckResponse.Count - 1); i++)
        {
            sum += splitUniqueCheckResponse[i].Percent;
        }

        return sum;
    }

    private double ArithmeticMean(
        double sum,
        IReadOnlyCollection<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        return sum / (splitUniqueCheckResponse.Count - 1.0);
    }

    private double ApplyCorrectionOfLastElement(
        double correctionPercent, double average, double currentPercent)
    {
        double plagiarismPercent = average;

        if (currentPercent > average)
        {
            plagiarismPercent += correctionPercent;
        }
        else if (currentPercent < average)
        {
            plagiarismPercent -= correctionPercent;
        }

        return plagiarismPercent;
    }

    private string MergeSplitText(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        string allText = string.Empty;

        foreach (var model in splitUniqueCheckResponse)
        {
            allText += model.Text;
        }

        return allText;
    }

    private List<HighlightRange> MergeHighlights(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse,
        IReadOnlyList<int> wordCountsSums)
    {
        List<HighlightRange> highlights = new();

        int i = 0;
        foreach (var model in splitUniqueCheckResponse)
        {
            foreach (var item in model.Highlights)
            {
                if (i == 0)
                {
                    highlights.AddRange(model.Highlights);
                }
                else
                {
                    HighlightRange highlight = new(
                        item.StartIndex + wordCountsSums[i - 1],
                        item.EndIndex + wordCountsSums[i - 1]
                    );
                    highlights.Add(highlight);
                }
            }

            i++;
        }

        return highlights;
    }

    private List<MatchedUrl> MergeMatchedUrls(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse,
        IReadOnlyList<int> wordCountsSums)
    {
        List<MatchedUrl> matchedUrls = new();

        int i = 0;
        foreach (PlagiarismCheckResponseModel model in splitUniqueCheckResponse)
        {
            foreach (MatchedUrl itemMatch in model.Matches)
            {
                List<HighlightRange> highlightRanges = new();
                foreach (HighlightRange itemHighlight in itemMatch.Highlights)
                {
                    if (i == 0)
                    {
                        highlightRanges.AddRange(itemMatch.Highlights);
                    }
                    else
                    {
                        HighlightRange highlight = new(
                            itemHighlight.StartIndex + wordCountsSums[i - 1],
                            itemHighlight.EndIndex + wordCountsSums[i - 1]
                        );
                        highlightRanges.Add(highlight);
                    }
                }

                MatchedUrl url = new(
                    itemMatch.Url,
                    itemMatch.Percent,
                    highlightRanges
                );
                matchedUrls.Add(url);
            }

            i++;
        }

        return matchedUrls;
    }

    private List<int> WordCountsNextElIsSumOfPrevious(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        IEnumerable<int> wordCounts = GetWordCounts(splitUniqueCheckResponse);
        
        int i = 0;
        int sum = 0;
        List<int> sums = new();
        
        foreach (var item in wordCounts)
        {
            if (i == 0)
            {
                sum = item;
                sums.Add(item);
            }
            else
            {
                sum += item;
                sums.Add(sum);
            }

            i++;
        }

        return sums;
    }

    private List<int> GetWordCounts(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        List<int> wordCounts = new();
        
        char[] delimiters = { ' ', '\r', '\n' };
        
        foreach (var response in splitUniqueCheckResponse)
        {
            int wordCount =
                response.Text.Split(
                    delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            
            wordCounts.Add(wordCount);
        }

        return wordCounts;
    }
    
    private List<string> GetSplitText(string text)
    {
        List<string> splitText = new();
        int endIndex = ApiInputRestriction;
        bool textIsNotEmpty = true;

        while (textIsNotEmpty)
        {
            if (text.Length <= ApiInputRestriction)
            {
                endIndex = text.Length;
                textIsNotEmpty = false;
            }

            if (text.Length <= 100)
            {
                continue;
            }

            string temp = text[..endIndex];
            splitText.Add(temp);
            
            text = text[endIndex..];
        }

        return splitText;
    }
}
