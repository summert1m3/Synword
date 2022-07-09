using Synword.Domain.Services.EnhancedRephrase;

namespace Synword.Domain.Interfaces.Services;

public interface IYandexTranslateApi
{
    public Task<TranslatedTextModel> Translate(
        string targetLanguageCode,
        string text);
}
