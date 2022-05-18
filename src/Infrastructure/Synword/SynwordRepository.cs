using Ardalis.Specification.EntityFrameworkCore;
using Synword.Domain.Interfaces;
using Synword.Domain.Interfaces.Repository;

namespace Synword.Infrastructure.Synword;

public class SynwordRepository<T> : 
    RepositoryBase<T>, ISynwordRepository<T> where T : class, IAggregateRoot
{
    public SynwordRepository(SynwordContext dbContext) : base(dbContext)
    {
    }
}
