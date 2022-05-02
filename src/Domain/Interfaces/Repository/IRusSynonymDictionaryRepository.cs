namespace Synword.Domain.Interfaces.Repository;

public interface IRusSynonymDictionaryRepository<T> : 
    IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
