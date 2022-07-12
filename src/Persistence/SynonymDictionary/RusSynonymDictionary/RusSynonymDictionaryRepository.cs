using Ardalis.Specification.EntityFrameworkCore;
using Synword.Domain.Interfaces;
using Synword.Domain.Interfaces.Repository;

namespace Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary;

public class RusSynonymDictionaryRepository<T> :
    RepositoryBase<T>, IRusSynonymDictionaryRepository<T> where T : class, IAggregateRoot
{
    public RusSynonymDictionaryRepository(RusSynonymDictionaryContext dbContext) : 
        base(dbContext)
    {
    }
}
