using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public sealed class UserWithRephraseHistorySpecification : Specification<User>, ISingleResultSpecification
{
    public UserWithRephraseHistorySpecification(string uId)
    {
        Query.Where(u => u.Id == uId).Include(u => u.RephraseHistory);
    }
}
