namespace Synword.Domain.Interfaces.Repository;

public interface ISynwordRepository<T> : 
    IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
