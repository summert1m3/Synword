using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Entities.TokenAggregate;

namespace Synword.Infrastructure.Identity;

public class AppIdentityDbContext : IdentityDbContext<AppUser>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
    }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
