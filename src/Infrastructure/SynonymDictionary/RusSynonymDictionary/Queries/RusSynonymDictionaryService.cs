using Microsoft.Extensions.DependencyInjection;
using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;

namespace Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary.Queries;

public class RusSynonymDictionaryService : IRusSynonymDictionaryService
{
    private static Dictionary<string, IReadOnlyList<Synonym>>? _rusSynonymDictionary;

    public IReadOnlyDictionary<string, IReadOnlyList<Synonym>> GetDictionary
    {
        get => _rusSynonymDictionary;
    }

    public static async void
        InitializeDictionary(IRusSynonymDictionaryRepository<Word> repository)
    {
        if (_rusSynonymDictionary != null)
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

        _rusSynonymDictionary = synonymDictionary;
    }
}
