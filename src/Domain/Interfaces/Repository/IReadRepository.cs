using Ardalis.Specification;

namespace Synword.Domain.Interfaces.Repository;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
