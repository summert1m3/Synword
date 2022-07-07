using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Entities.HistoryAggregate;
using Synword.Domain.Entities.PlagiarismCheckAggregate;
using Synword.Domain.Entities.RephraseAggregate;
using Synword.Domain.Entities.UserAggregate;
using Synword.Infrastructure.Synword.Config;

namespace Synword.Infrastructure.Synword;

public class SynwordContext : DbContext
{
    public SynwordContext(DbContextOptions<SynwordContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<History> Histories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<UsageData> UsageData { get; set; }
    public DbSet<PlagiarismCheckResult> PlagiarismCheckHistories { get; set; }
    public DbSet<MatchedUrl> MatchedUrls { get; set; }
    public DbSet<HighlightRange> HighlightRanges { get; set; }
    public DbSet<RephraseResult> RephraseHistories { get; set; }
    public DbSet<SourceWordSynonyms> SourceWordSynonyms { get; set; }
    public DbSet<Synonym> Synonyms { get; set; }
    public DbSet<Metadata> Metadata { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new SynonymConfiguration());
        builder.ApplyConfiguration(new MetadataConfiguration());
        
        builder.Entity<RephraseResult>().Navigation(
            r => r.History).AutoInclude();
        builder.Entity<RephraseResult>().Navigation(
            r => r.Synonyms).AutoInclude();
        builder.Entity<SourceWordSynonyms>().Navigation(
            r => r.Synonyms).AutoInclude();
        
        builder.Entity<PlagiarismCheckResult>().Navigation(
            r => r.History).AutoInclude();
        builder.Entity<PlagiarismCheckResult>().Navigation(
            r => r.Highlights).AutoInclude();
        builder.Entity<PlagiarismCheckResult>().Navigation(
            r => r.Matches).AutoInclude();
        builder.Entity<MatchedUrl>().Navigation(
            r => r.Highlights).AutoInclude();
    }
}
