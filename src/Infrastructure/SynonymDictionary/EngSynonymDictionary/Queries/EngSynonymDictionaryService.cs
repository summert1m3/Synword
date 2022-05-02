using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary.Queries;

public class EngSynonymDictionaryService : IEngSynonymDictionaryService
{
    private static Dictionary<string, IReadOnlyList<Synonym>>? _engSynonymDictionary;

    public IReadOnlyDictionary<string, IReadOnlyList<Synonym>> GetDictionary
    {
        get => _engSynonymDictionary;
    }

    public static async Task
        InitializeDictionary(IEngSynonymDictionaryRepository<Word> repository)
    {
        if (_engSynonymDictionary != null)
        {
            return;
        }

        List<Word> words = await repository.ListAsync();
        
        Dictionary<string, IReadOnlyList<Synonym>> synonymDictionary = new();

        foreach (var word in words)
        {
            synonymDictionary.Add(
                word.Value,
                word.Synonyms.AsReadOnly()
            );
        }

        _engSynonymDictionary = synonymDictionary;
    }
}
