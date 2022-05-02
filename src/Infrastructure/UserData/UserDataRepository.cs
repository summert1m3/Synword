using Ardalis.Specification.EntityFrameworkCore;
using Synword.Domain.Interfaces;
using Synword.Domain.Interfaces.Repository;

namespace Synword.Infrastructure.UserData;

public class UserDataRepository<T> : 
    RepositoryBase<T>, IUserDataRepository<T> where T : class, IAggregateRoot
{
    public UserDataRepository(UserDataContext dbContext) : base(dbContext)
    {
    }
}
