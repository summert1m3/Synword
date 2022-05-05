using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Services.Rephrase;

namespace Synword.Domain.Interfaces.Services;

public interface IRephraseService
{
    public RephraseResult Rephrase(
        string text, ISynonymDictionaryService dictionary);
}
