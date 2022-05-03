using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate;

namespace Synword.Infrastructure.UserData;

public class UserDataContext : DbContext
{
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<UsageData> UsageData { get; set; }
    public DbSet<PlagiarismCheckResult> PlagiarismCheckHistories { get; set; }
    public DbSet<MatchedUrl> MatchedUrls { get; set; }
    public DbSet<HighlightRange> HighlightRanges { get; set; }
    public DbSet<RephraseResult> RephraseHistories { get; set; }
    public DbSet<Synonym> Synonyms { get; set; }
    public DbSet<Metadata> Metadata { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<User>().Navigation(u => u.UsageData).AutoInclude();
    }
}
