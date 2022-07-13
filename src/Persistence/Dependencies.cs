using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synword.Domain.Interfaces.Repository;
using Synword.Persistence.Identity;
using Synword.Persistence.SynonymDictionary.EngSynonymDictionary;
using Synword.Persistence.SynonymDictionary.RusSynonymDictionary;
using Synword.Persistence.Synword;

namespace Synword.Persistence;

public static class Dependencies
{
    public static void AddPersistence(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped(
            typeof(ISynwordRepository<>), 
            typeof(SynwordRepository<>));
    
        services.AddScoped(
            typeof(IRusSynonymDictionaryRepository<>),
            typeof(RusSynonymDictionaryRepository<>));
    
        services.AddScoped(
            typeof(IEngSynonymDictionaryRepository<>),
            typeof(EngSynonymDictionaryRepository<>));
        
        services.AddDbContext<SynwordContext>(c =>
            c.UseSqlite(configuration["UserDataDbConnection"]));
        
        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlite(configuration["IdentityDbConnection"]));
        
        services.AddDbContext<RusSynonymDictionaryContext>(options =>
            options.UseSqlite(configuration["RusSynonymDictionaryDbConnection"]));
        
        services.AddDbContext<EngSynonymDictionaryContext>(options =>
            options.UseSqlite(configuration["EngSynonymDictionaryDbConnection"]));
    }
}
