using Microsoft.EntityFrameworkCore;

namespace Synword.Infrastructure.Data;

public class PricesContext : DbContext
{
    public PricesContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }
}
