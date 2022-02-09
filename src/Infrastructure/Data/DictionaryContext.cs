using Microsoft.EntityFrameworkCore;

namespace Synword.Infrastructure.Data;

public class DictionaryContext : DbContext
{
    public DictionaryContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }
}
