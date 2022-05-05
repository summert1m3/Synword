using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary.Queries;

public class RusSynonymDictionaryService : ISynonymDictionaryService
{
    private static Dictionary<string, IReadOnlyList<DictionarySynonym>>? _rusSynonymDictionary;

    public IReadOnlyDictionary<string, IReadOnlyList<DictionarySynonym>> GetDictionary
    {
        get => _rusSynonymDictionary;
    }

    public static async Task
        InitializeDictionary(IRusSynonymDictionaryRepository<Word> repository)
    {
        if (_rusSynonymDictionary != null)
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

        _rusSynonymDictionary = synonymDictionary;
    }
}
