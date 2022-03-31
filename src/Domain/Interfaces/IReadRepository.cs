using Ardalis.Specification;

namespace Synword.Domain.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
