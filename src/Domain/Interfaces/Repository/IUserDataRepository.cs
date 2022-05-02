namespace Synword.Domain.Interfaces.Repository;

public interface IUserDataRepository<T> : 
    IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
