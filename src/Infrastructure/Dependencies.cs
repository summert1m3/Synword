using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary;
using Synword.Infrastructure.UserData;

namespace Synword.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<UserDataContext>(c =>
            c.UseSqlite(configuration["UserDataDbConnection"]));
        
        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlite(configuration["IdentityDbConnection"]));
        
        services.AddDbContext<RusSynonymDictionaryContext>(options =>
            options.UseSqlite(configuration["RusSynonymDictionaryDbConnection"]));
        
        services.AddDbContext<EngSynonymDictionaryContext>(options =>
            options.UseSqlite(configuration["EngSynonymDictionaryDbConnection"]));
    }
}
