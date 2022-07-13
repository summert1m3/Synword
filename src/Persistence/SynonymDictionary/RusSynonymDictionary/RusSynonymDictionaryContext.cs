using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Entities.SynonymDictionaryAggregate;

namespace Synword.Persistence.SynonymDictionary.RusSynonymDictionary;

public class RusSynonymDictionaryContext : DbContext
{
    public RusSynonymDictionaryContext(
        DbContextOptions<RusSynonymDictionaryContext> options) : base(options)
    {
    }
    
    public DbSet<Word> Words { get; set; }
    public DbSet<DictionarySynonym> Synonyms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<Word>().Navigation(u => u.Synonyms).AutoInclude();
    }
}
