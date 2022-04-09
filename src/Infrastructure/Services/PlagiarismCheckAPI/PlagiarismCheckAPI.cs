using Microsoft.Extensions.Configuration;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces;
using Synword.Domain.Services.PlagiarismCheck;

namespace Synword.Infrastructure.Services.PlagiarismCheckAPI;

public class PlagiarismCheckAPI : IPlagiarismCheckAPI
{
    private readonly string _apiKey;
    private readonly string _apiUrl;
    private readonly HttpClient _httpClient = new();

    public PlagiarismCheckAPI(IConfiguration configuration)
    {
        _apiKey ??= configuration["PlagiarismCheckApiKey"];
        _apiUrl ??= configuration["PlagiarismCheckApiUrl"];
    }
    
    public async Task<PlagiarismCheckResponseModel> CheckPlagiarism(string text)
    {
        Dictionary<string, string> values = new Dictionary<string, string> {
            { "key", _apiKey },
            { "text", text },
            { "test", true.ToString() }
        };
        
        HttpResponseMessage response = await _httpClient.PostAsync(
            _apiUrl, 
            new FormUrlEncodedContent(values)
            );

        if (!response.IsSuccessStatusCode) {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        string responseString = await response.Content.ReadAsStringAsync();
        PlagiarismCheckResponseModel plagiarismCheckResponseModel = 
            responseString.FromJson<PlagiarismCheckResponseModel>();

        if (plagiarismCheckResponseModel.Error != string.Empty) {
            throw new Exception(plagiarismCheckResponseModel.Error);
        }

        return plagiarismCheckResponseModel;
    }
}
