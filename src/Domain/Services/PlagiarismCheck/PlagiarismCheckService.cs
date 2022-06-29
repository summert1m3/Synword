using Microsoft.Extensions.Logging;
using Synword.Domain.Constants;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Interfaces.Services;

namespace Synword.Domain.Services.PlagiarismCheck;

public class PlagiarismCheckService : IPlagiarismCheckService
{
    private readonly IPlagiarismCheckAPI _plagiarismCheckApi;
    private readonly ILogger<PlagiarismCheckService> _logger;

    public PlagiarismCheckService(IPlagiarismCheckAPI plagiarismCheckApi, 
        ILogger<PlagiarismCheckService> logger)
    {
        _plagiarismCheckApi = plagiarismCheckApi;
        _logger = logger;
    }

    public async Task<PlagiarismCheckResult> CheckPlagiarism(string text)
    {
        PlagiarismCheckResult result;
        
        if (text.Length <= ExternalServicesConstants.ApiInputRestriction)
        {
            result = await CheckPlagiarismUnderLimit(text);
        }
        else
        {
            result = await CheckPlagiarismOverLimit(text);
        }

        return result;
    }
    
    private async Task<PlagiarismCheckResult> CheckPlagiarismUnderLimit(string text)
    {
        return await _plagiarismCheckApi.CheckPlagiarism(text);
    }

    private async Task<PlagiarismCheckResult> CheckPlagiarismOverLimit(string text)
    {
        List<string> splitText = GetSplitText(text);

        List<PlagiarismCheckResult> splitPlagiarismCheckResponse =
            await CheckPlagiarismForSplitText(splitText);

        float plagiarismPercent = PercentCorrection(
            splitPlagiarismCheckResponse, splitText);

        float roundedPlagiarismPercent = 
            (float)Math.Round(plagiarismPercent, 1);
        
        string allText = MergeSplitText(splitPlagiarismCheckResponse);

        List<int> wordCountsSums = 
            WordCountsNextElIsSumOfPrevious(splitPlagiarismCheckResponse);
        
        List<HighlightRange> highlights = MergeHighlights(
            splitPlagiarismCheckResponse,
            wordCountsSums);

        List<MatchedUrl> matchedUrls = MergeMatchedUrls(
            splitPlagiarismCheckResponse,
            wordCountsSums);

        PlagiarismCheckResult result = new(
            allText,
            roundedPlagiarismPercent,
            highlights,
            matchedUrls
        );

        return result;
    }

    private async Task<List<PlagiarismCheckResult>> 
        CheckPlagiarismForSplitText(IEnumerable<string> splitText)
    {
        List<PlagiarismCheckResult> splitPlagiarismCheckResponse = new();
        
        List<Task<PlagiarismCheckResult>> tasks = new();
        
        foreach (var text in splitText)
        {
            tasks.Add(_plagiarismCheckApi.CheckPlagiarism(text));
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            splitPlagiarismCheckResponse.Add(task.Result);
        }

        return splitPlagiarismCheckResponse;
    }

    private float PercentCorrection(
        IReadOnlyList<PlagiarismCheckResult> splitPlagiarismCheckResponse,
        IReadOnlyList<string> splitText)
    {
        double percentRatioFromOtherParts =
            CalculatePercentLastElementFromOtherParts(splitText);

        double sum = SumOfPercentWithoutLastElement(
            splitPlagiarismCheckResponse);

        double average = ArithmeticMean(sum, splitPlagiarismCheckResponse);
        
        double correctionPercent = LastElementPercentCorrection(
            average,
            splitPlagiarismCheckResponse.Last().Percent,
            percentRatioFromOtherParts);

        float plagiarismPercent = (float)ApplyCorrectionOfLastElement(
            correctionPercent, 
            average, 
            splitPlagiarismCheckResponse.Last().Percent);

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
        IReadOnlyList<PlagiarismCheckResult> splitPlagiarismCheckResponse)
    {
        double sum = 0;

        for (int i = 0; i < (splitPlagiarismCheckResponse.Count - 1); i++)
        {
            sum += splitPlagiarismCheckResponse[i].Percent;
        }

        return sum;
    }

    private double ArithmeticMean(
        double sum,
        IReadOnlyCollection<PlagiarismCheckResult> splitPlagiarismCheckResponse)
    {
        return sum / (splitPlagiarismCheckResponse.Count - 1.0);
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
        IEnumerable<PlagiarismCheckResult> splitPlagiarismCheckResponse)
    {
        string allText = string.Empty;

        foreach (var model in splitPlagiarismCheckResponse)
        {
            allText += model.Text;
        }

        return allText;
    }

    private List<HighlightRange> MergeHighlights(
        IEnumerable<PlagiarismCheckResult> splitPlagiarismCheckResponse,
        IReadOnlyList<int> wordCountsSums)
    {
        List<HighlightRange> highlights = new();

        int i = 0;
        foreach (var model in splitPlagiarismCheckResponse)
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
        IEnumerable<PlagiarismCheckResult> splitPlagiarismCheckResponse,
        IReadOnlyList<int> wordCountsSums)
    {
        List<MatchedUrl> matchedUrls = new();

        int i = 0;
        foreach (var model in splitPlagiarismCheckResponse)
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
        IEnumerable<PlagiarismCheckResult> splitPlagiarismCheckResponse)
    {
        IEnumerable<int> wordCounts = GetWordCounts(splitPlagiarismCheckResponse);
        
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
        IEnumerable<PlagiarismCheckResult> splitPlagiarismCheckResponse)
    {
        List<int> wordCounts = new();
        
        char[] delimiters = { ' ', '\r', '\n' };
        
        foreach (var response in splitPlagiarismCheckResponse)
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
        int endIndex = ExternalServicesConstants.ApiInputRestriction;
        bool textIsNotEmpty = true;

        while (textIsNotEmpty)
        {
            if (text.Length <= ExternalServicesConstants.ApiInputRestriction)
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
