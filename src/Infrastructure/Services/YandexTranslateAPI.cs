using System.Net.Http.Headers;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Synword.Domain.Extensions;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.EnhancedRephrase;
using Synword.Domain.Services.Rephrase;

namespace Synword.Infrastructure.Services;

public class YandexTranslateApi : IYandexTranslateApi
{
    private const string ApiUrl
        = "https://translate.api.cloud.yandex.net/translate/v2/translate";

    private const string GetIAmTokenUrl
        = "https://iam.api.cloud.yandex.net/iam/v1/tokens";

    private readonly string _folderId;
    private static readonly HttpClient _httpClient = new();
    private readonly string? _oAuthToken;
    private static string? _iAmToken;
    private static DateTime? _tokenAssignDate;
    private static DateTime? _tokenExpirationDate;
    private const int TokenLifetime = 1;

    public YandexTranslateApi(IConfiguration configuration)
    {
        _oAuthToken = configuration["OAuthToken"];
        _folderId = configuration["FolderId"];
    }

    public async Task<TranslatedTextModel> Translate(string targetLanguageCode, string text)
    {
        if (_tokenAssignDate is null)
        {
            await InitializeIAmToken();
        }
        else if (IsTokenExpired())
        {
            await InitializeIAmToken();
        }

        var response = await RequestAsync(targetLanguageCode, text);

        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();
        TranslateModelRaw translateModel =
            responseString.FromJson<TranslateModelRaw>();

        return new TranslatedTextModel()
        {
            Text = translateModel.Translations.First().Text,
            DetectedLanguageCode =
                translateModel.Translations.First().DetectedLanguageCode
        };
    }

    private async Task<HttpResponseMessage> RequestAsync(
        string targetLanguageCode, string text)
    {
        var request = new HttpRequestMessage() {RequestUri = new Uri(ApiUrl), Method = HttpMethod.Post,};

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", _iAmToken);

        List<string> texts = new() {text};

        Dictionary<string, object> values =
            new Dictionary<string, object>
            {
                {"texts", texts}, {"targetLanguageCode", targetLanguageCode}, {"folderId", _folderId}
            };

        string json = values.ToJson();

        request.Content = new StringContent(values.ToJson());

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        return response;
    }

    private async Task<IAmTokenModel> GetIAMToken()
    {
        Guard.Against.Null(_oAuthToken, nameof(_oAuthToken));

        Dictionary<string, string> values =
            new Dictionary<string, string> {{"yandexPassportOauthToken", _oAuthToken},};

        HttpResponseMessage response = await _httpClient.PostAsync(
            GetIAmTokenUrl,
            new StringContent(values.ToJson())
        );

        string responseString = await response.Content.ReadAsStringAsync();

        IAmTokenModel token = responseString.FromJson<IAmTokenModel>();
        
        return token;
    }

    private bool IsTokenExpired()
    {
        return _tokenAssignDate > _tokenExpirationDate;
    }

    private async Task InitializeIAmToken()
    {
        var token = await GetIAMToken();
        _iAmToken = token.IAmToken;
        
        _tokenAssignDate = DateTime.Now;
        _tokenExpirationDate = DateTime.Now.AddHours(TokenLifetime);
    }
}

internal class TranslateModelRaw
{
    public List<TranslatedTextsRaw> Translations { get; set; }
}

internal class TranslatedTextsRaw
{
    public string Text { get; set; }
    public string DetectedLanguageCode { get; set; }
}

internal class IAmTokenModel
{
    public string IAmToken { get; init; }
}
