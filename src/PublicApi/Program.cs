using System.Data.SQLite;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint.Extensions;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Synword.Application.AutoMapper;
using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Infrastructure.SynonymDictionary;
using Synword.Persistence.Entities.Identity;
using Synword.Persistence.Identity;
using Synword.Persistence.Synword;
using Synword.PublicApi;
using Synword.PublicApi.Middleware;

LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

Synword.Persistence.Dependencies
    .AddPersistence(builder.Configuration, builder.Services);

Synword.Infrastructure.Dependencies
    .AddInfrastructure(builder.Configuration, builder.Services);

Synword.Application.Dependencies
    .AddApplication(builder.Configuration, builder.Services);

builder.Services.AddControllers();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddJwtBearerAuthentication(builder.Configuration);

builder.Services.AddDomainServices();

builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddAutoMapper(typeof(DomainProfile));

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

await InitLogsDb();

app.Logger.LogInformation("PublicApi App created...");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders 
        = ForwardedHeaders.XForwardedFor 
          | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using var scope = app.Services.CreateScope();
var scopedProvider = scope.ServiceProvider;

ApplyMigrations();
await SeedDatabase();
await InitializeDictionaries();

app.MapEndpoints();
app.Logger.LogInformation("LAUNCHING PublicApi");
app.Logger.LogInformation("Swagger: http://localhost:5000/swagger");
app.Run();



void ApplyMigrations()
{
    app.Logger.LogInformation("Applying migrations...");
    
    try
    {
        var synwordDb = scopedProvider.GetRequiredService<SynwordContext>();
        synwordDb.Database.Migrate();
        
        var identityDb = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        identityDb.Database.Migrate();
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred applying migrations");
    }
}

async Task SeedDatabase()
{
    app.Logger.LogInformation("Seeding Database...");
    
    try
    {
        var userManager = scopedProvider.
            GetRequiredService<UserManager<UserIdentity>>();
        var roleManager = scopedProvider.
            GetRequiredService<RoleManager<IdentityRole>>();
        await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding database");
    }
}

async Task InitializeDictionaries()
{
    app.Logger.LogInformation("Initialize synonym dictionary...");
    
    try
    {
        var rusDictionaryDb = 
            scopedProvider.
                GetRequiredService<IRusSynonymDictionaryRepository<Word>>();
        
        await RusSynonymDictionaryService.InitializeDictionary(rusDictionaryDb);
        
        var engDictionaryDb = 
            scopedProvider.
                GetRequiredService<IEngSynonymDictionaryRepository<Word>>();
        
        await EngSynonymDictionaryService.InitializeDictionary(engDictionaryDb);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred initializing dictionaries");
    }
}

async Task InitLogsDb()
{
    if (!File.Exists(@"./LogsDB.sqlite"))
    {
        Console.WriteLine("Creating logs database...");
        
        SQLiteConnection.CreateFile(@"./LogsDB.sqlite");

        await using var dbConnection = new SQLiteConnection(@"Data Source=LogsDB.sqlite");
        dbConnection.Open();
        
        string sql = @"CREATE TABLE ""Logs"" ( 
                `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                `TimestampUtc` TEXT NOT NULL, 
                `Application` TEXT NOT NULL, 
                `Level` TEXT NOT NULL, 
                `Message` TEXT NOT NULL, 
                `Logger` TEXT NOT NULL, 
                `Exception` TEXT )";
        
        SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
        command.ExecuteNonQuery();
    }
    else
    {
        await using var dbConnection = new SQLiteConnection(@"Data Source=LogsDB.sqlite");
        dbConnection.Open();
        
        var sqlQuery = "SELECT name FROM sqlite_master WHERE name='Logs'";
        
        SQLiteCommand command = new SQLiteCommand(sqlQuery, dbConnection);
        var name = command.ExecuteScalar();

        if (name is null || name.ToString() != "Logs")
        {
            string sqlCreate = @"CREATE TABLE ""Logs"" ( 
                `Id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                `TimestampUtc` TEXT NOT NULL, 
                `Application` TEXT NOT NULL, 
                `Level` TEXT NOT NULL, 
                `Message` TEXT NOT NULL, 
                `Logger` TEXT NOT NULL, 
                `Exception` TEXT )";
        
            SQLiteCommand createCommand = new SQLiteCommand(sqlCreate, dbConnection);
            createCommand.ExecuteNonQuery();
        }
    }
}
