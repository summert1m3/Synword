using Ardalis.Specification.EntityFrameworkCore;
using Synword.Domain.Interfaces;
using Synword.Domain.Interfaces.Repository;

namespace Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary;

public class EngSynonymDictionaryRepository<T> :
    RepositoryBase<T>, IEngSynonymDictionaryRepository<T> 
        where T : class, IAggregateRoot
{
    public EngSynonymDictionaryRepository(EngSynonymDictionaryContext dbContext) : 
        base(dbContext)
    {
    }
}
