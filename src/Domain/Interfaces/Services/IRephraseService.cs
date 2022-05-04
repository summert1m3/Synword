using Synword.Domain.Entities.RephraseAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface IRephraseService
{
    public RephraseResult Rephrase(
        string text, ISynonymDictionaryService dictionary);
}
