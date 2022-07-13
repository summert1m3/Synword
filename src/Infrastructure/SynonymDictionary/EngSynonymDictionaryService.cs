using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Synword.Infrastructure.SynonymDictionary;

public class EngSynonymDictionaryService : ISynonymDictionaryService
{
    private static Dictionary<string, IReadOnlyList<DictionarySynonym>>? 
        _engSynonymDictionary;

    public IReadOnlyDictionary<string, IReadOnlyList<DictionarySynonym>> GetDictionary
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
        
        Dictionary<string, IReadOnlyList<DictionarySynonym>> synonymDictionary = new();

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
