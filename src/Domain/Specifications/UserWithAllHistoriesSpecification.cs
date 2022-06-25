using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public sealed class UserWithAllHistoriesSpecification : Specification<User>, ISingleResultSpecification
{
    public UserWithAllHistoriesSpecification(string uId)
    {
        Query.Where(u => u.Id == uId).Include(u => u.RephraseHistory)
            .Include(u => u.PlagiarismCheckHistory);
    }
}
