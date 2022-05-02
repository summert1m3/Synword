using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Domain.Interfaces.Services;

public interface IEngSynonymDictionaryService
{
    IReadOnlyDictionary<string, IReadOnlyList<Synonym>> GetDictionary { get; }
}
