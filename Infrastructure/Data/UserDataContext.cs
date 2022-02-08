﻿using Microsoft.EntityFrameworkCore;
using Synword.ApplicationCore.Entities.UserAggregate;

namespace Synword.Infrastructure.Data;

public class UserDataContext : DbContext
{
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<UsageData> UsageData { get; set; }
    public DbSet<PlagiarismCheckHistory> PlagiarismCheckHistories { get; set; }
    public DbSet<MatchedUrl> Matches { get; set; }
    public DbSet<HighlightRange> HighlightRanges { get; set; }
    public DbSet<RephraseHistory> RephraseHistories { get; set; }
    public DbSet<Synonym> Synonyms { get; set; }
    public DbSet<Metadata> Metadata { get; set; }
}
