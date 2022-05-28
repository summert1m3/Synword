using System.Collections.Generic;
using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Services;

namespace UnitTests.Domain.Services.RephraseTests;

public class SynonymDictionaryServiceStub : ISynonymDictionaryService
{
    public IReadOnlyDictionary<string, IReadOnlyList<DictionarySynonym>>
        GetDictionary
    {
        get => new Dictionary<string, IReadOnlyList<DictionarySynonym>>()
        {
            {
                "lorem", 
                new []
            {
                new DictionarySynonym("velit")
            }
                
            },
            {
                "dolor", 
                new []
                {
                    new DictionarySynonym("lorem")
                }
                
            },
            {
                "consectetur", 
                new []
                {
                    new DictionarySynonym("sint")
                }
                
            },
            {
                "aute", 
                new []
                {
                    new DictionarySynonym("consectetur")
                }
                
            },
        };
    }
}
