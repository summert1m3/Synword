using Synword.Domain.Interfaces.Services;

namespace Synword.Domain.Services.EnhancedRephrase;

public class EnhancedRephraseService : IEnhancedRephraseService
{
    private readonly IYandexTranslateApi _api;
    private readonly string _targetLanguageCode = "de";

    public EnhancedRephraseService(IYandexTranslateApi api)
    {
        _api = api;
    }
    
    public async Task<EnhancedRephraseResult> Rephrase(string text)
    {
        TranslatedTextModel translatedText = await _api.Translate(
            _targetLanguageCode, text);

        TranslatedTextModel rephrasedText = await _api.Translate(
            translatedText.DetectedLanguageCode, 
            translatedText.Text);

        return new EnhancedRephraseResult(rephrasedText.Text);
    }
}
