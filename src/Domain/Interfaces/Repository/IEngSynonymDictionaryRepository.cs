namespace Synword.Domain.Interfaces.Repository;

public interface IEngSynonymDictionaryRepository<T> : 
    IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
