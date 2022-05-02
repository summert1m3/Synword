using Ardalis.Specification;

namespace Synword.Domain.Interfaces.Repository;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
