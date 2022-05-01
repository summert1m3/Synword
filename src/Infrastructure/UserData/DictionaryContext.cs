using Microsoft.EntityFrameworkCore;

namespace Synword.Infrastructure.UserData;

public class DictionaryContext : DbContext
{
    public DictionaryContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }
}
