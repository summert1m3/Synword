using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Interfaces.Services;

namespace Synword.Domain.Services.Rephrase;

public class RephraseService : IRephraseService
{
    public RephraseResult Rephrase(
        string text, ISynonymDictionaryService dictionary)
    {
        throw new NotImplementedException();
    }
}
