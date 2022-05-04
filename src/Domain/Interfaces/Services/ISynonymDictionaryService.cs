using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface ISynonymDictionaryService
{
    IReadOnlyDictionary<string, IReadOnlyList<Synonym>> GetDictionary { get; }
}
