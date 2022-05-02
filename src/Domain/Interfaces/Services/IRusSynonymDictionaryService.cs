using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface IRusSynonymDictionaryService
{
    IReadOnlyDictionary<string, IReadOnlyList<Synonym>> GetDictionary { get; }
}
