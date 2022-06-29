using Synword.Domain.Services.EnhancedRephrase;
using Synword.Domain.Services.Rephrase;

namespace Synword.Domain.Interfaces.Services;

public interface IYandexTranslateApi
{
    public Task<TranslatedTextModel> Translate(
        string targetLanguageCode,
        string text);
}
