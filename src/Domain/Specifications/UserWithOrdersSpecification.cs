using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public sealed class UserWithOrdersSpecification : Specification<User>, ISingleResultSpecification
{
    public UserWithOrdersSpecification(string uId)
    {
        Query.Where(u => u.Id == uId).Include(u => u.Orders);
    }
}
