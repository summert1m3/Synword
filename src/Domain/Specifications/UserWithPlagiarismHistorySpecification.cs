using Ardalis.Specification;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Domain.Specifications;

public sealed class UserWithPlagiarismHistorySpecification : Specification<User>, ISingleResultSpecification
{
    public UserWithPlagiarismHistorySpecification(string uId)
    {
        Query.Where(u => u.Id == uId).Include(u => u.PlagiarismCheckHistory);
    }
}
