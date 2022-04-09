using Synword.Domain.Entities.UserAggregate;
using Synword.Domain.Interfaces;

namespace Synword.Domain.Services.PlagiarismCheck;

public class PlagiarismCheckService
{
    private readonly IPlagiarismCheckAPI _plagiarismCheckApi;
    private readonly int _apiInputRestriction = 20000;

    public PlagiarismCheckService(IPlagiarismCheckAPI plagiarismCheckApi)
    {
        _plagiarismCheckApi = plagiarismCheckApi;
    }
    
    public async Task<PlagiarismCheckResponseModel> CheckPlagiarism(string text)
    {
        PlagiarismCheckResponseModel model = new();
        
        if (text.Length <= _apiInputRestriction)
        {
            model = await CheckPlagiarismUnderLimit(text);
        }
        else if (text.Length > _apiInputRestriction)
        {
            model = await CheckPlagiarismOverLimit(text);
        }

        return model;
    }

    private async Task<List<PlagiarismCheckResponseModel>> CheckPlagiarismForSplitText(
        IEnumerable<string> splitText)
    {
        List<PlagiarismCheckResponseModel> splitUniqueCheckResponse = new();
        foreach (var text in splitText)
        {
            PlagiarismCheckResponseModel uniqueCheckResponse 
                = await _plagiarismCheckApi.CheckPlagiarism(text);
            splitUniqueCheckResponse.Add(uniqueCheckResponse);
        }

        return splitUniqueCheckResponse;
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

    private double PercentCorrection(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse, 
        double percentRatioFromOtherParts)
    {
        double correction = ((splitUniqueCheckResponse.Last().Percent / 100) 
                             * percentRatioFromOtherParts);
        
        return correction;
    }

    private double SumOfPercentWithoutLastElement(
        IReadOnlyList<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        double sum = 0;
        
        for (int i = 0; i < (splitUniqueCheckResponse.Count - 2); i++) {
            sum += splitUniqueCheckResponse[i].Percent;
        }

        return sum;
    }

    private double ArithmeticMean(
        double sum,
        IReadOnlyCollection<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        return sum / splitUniqueCheckResponse.Count - 1;
    }
    
    private double ApplyCorrectionOfLastElement(double correction, double average)
    {
        double plagiarismPercent = average;
        
        if (correction > average)
        {
            plagiarismPercent += correction;
        }
        else if (correction < average)
        {
            plagiarismPercent -= correction;
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
            allText += " ";
        }

        return allText;
    }

    private List<HighlightRange> MergeHighlights(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        List<HighlightRange> highlights = new();
        
        foreach (var model in splitUniqueCheckResponse)
        {
            int count = _apiInputRestriction;
            if (count > _apiInputRestriction)
            {
                foreach (var item in model.Highlights)
                {
                    HighlightRange highlight = new(
                        item.StartIndex + count,
                        item.EndIndex + count
                    );
                    highlights.Add(highlight);
                }
            }
            count += _apiInputRestriction;
        }

        return highlights;
    }

    private List<MatchedUrl> MergeMatchedUrls(
        IEnumerable<PlagiarismCheckResponseModel> splitUniqueCheckResponse)
    {
        List<MatchedUrl> matchedUrls = new();
        foreach (PlagiarismCheckResponseModel model in splitUniqueCheckResponse)
        {
            int count = _apiInputRestriction;
            if (count > _apiInputRestriction)
            {
                foreach (MatchedUrl itemMatch in model.Matches)
                {
                    List<HighlightRange> highlightRanges = new();
                    foreach (HighlightRange itemHighlight in itemMatch.Highlights)
                    {
                        HighlightRange highlight = new(
                            itemHighlight.StartIndex + count,
                            itemHighlight.EndIndex + count
                        );
                                
                        highlightRanges.Add(highlight);
                    }

                    MatchedUrl url = new(
                        itemMatch.Url,
                        itemMatch.Percent,
                        highlightRanges
                    );
                    matchedUrls.Add(url);
                }
            }

            count += _apiInputRestriction;
        }

        return matchedUrls;
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

        double percentRatioFromOtherParts = 
            CalculatePercentLastElementFromOtherParts(splitText);
                
        double correction = PercentCorrection(
            splitUniqueCheckResponse, 
            percentRatioFromOtherParts);

        double sum = SumOfPercentWithoutLastElement(
            splitUniqueCheckResponse);
                
        double average = ArithmeticMean(sum, splitUniqueCheckResponse);

        float plagiarismPercent = (float)ApplyCorrectionOfLastElement(correction, average);
        
        string allText = MergeSplitText(splitUniqueCheckResponse);
        
        List<HighlightRange> highlights = MergeHighlights(splitUniqueCheckResponse);
        
        List<MatchedUrl> matchedUrls = MergeMatchedUrls(splitUniqueCheckResponse);

        PlagiarismCheckResponseModel model = new()
        {
            Error = string.Empty,
            Highlights = highlights,
            Matches = matchedUrls,
            Percent = plagiarismPercent,
            Text = allText
        };

        return model;
    }

    private List<string> GetSplitText(string text) {
        List<string> splitText = new List<string>();
        int endIndex = _apiInputRestriction;
        bool textIsNotEmpty = true;
        
        while (textIsNotEmpty) {
            
            if (text.Length <= _apiInputRestriction) {
                endIndex = text.Length;
                textIsNotEmpty = false;
            }

            if (text.Length > 100) {
                string temp = text.Substring(0, endIndex);
                splitText.Add(temp);

                text = text.Substring(endIndex);
            }
        }

        return splitText;
    }
}
