using Ardalis.GuardClauses;
using Synword.Domain.Interfaces;

namespace Synword.Domain.Entities.SynonymDictionaryAggregate;

public class DictionarySynonym : BaseEntity, IAggregateRoot
{
    private DictionarySynonym()
    {
        // required by EF
    }
    public DictionarySynonym(string value)
    {
        Guard.Against.NullOrEmpty(value, nameof(value));

        Value = value;
    }
    
    public string Value { get; private set; }
}
