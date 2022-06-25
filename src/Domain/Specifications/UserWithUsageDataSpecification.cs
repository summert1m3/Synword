using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public sealed class UserWithUsageDataSpecification : Specification<User>, ISingleResultSpecification
{
    public UserWithUsageDataSpecification(string uId)
    {
        Query.Where(u => u.Id == uId).Include(u => u.UsageData);
    }
}
