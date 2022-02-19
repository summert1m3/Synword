using Ardalis.Specification;

namespace Synword.ApplicationCore.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
