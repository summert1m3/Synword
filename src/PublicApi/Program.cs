using Application.AutoMapper;
using Application.Guests.Services;
using Application.PlagiarismCheck.Services;
using Application.Rephrase;
using Application.Users.Services;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint.Extensions;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Entities.SynonymDictionaryAggregate;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.PlagiarismCheck;
using Synword.Domain.Services.Rephrase;
using Synword.Infrastructure.Services.PlagiarismCheckAPI;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary.Queries;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary.Queries;
using Synword.Infrastructure.UserData;
using Synword.PublicApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

Synword.Infrastructure.Dependencies
    .ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllers();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwagger();

builder.Services.AddJwtBearerAuthentication(builder.Configuration);

AddCustomServices();

var app = builder.Build();

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



void AddCustomServices()
{
    AddRepositories();
    
    AddAppServices();
    
    builder.Services.AddSingleton(builder.Configuration);

    builder.Services.AddAutoMapper(typeof(DomainProfile));
}

void AddRepositories()
{
    builder.Services.AddScoped(
        typeof(IUserDataRepository<>), 
        typeof(UserDataRepository<>));
    
    builder.Services.AddScoped(
        typeof(IRusSynonymDictionaryRepository<>),
        typeof(RusSynonymDictionaryRepository<>));
    
    builder.Services.AddScoped(
        typeof(IEngSynonymDictionaryRepository<>),
        typeof(EngSynonymDictionaryRepository<>));
}

void AddAppServices()
{
    builder.Services.AddScoped(
        typeof(IGoogleApi), typeof(GoogleApi));
    
    builder.Services.AddScoped(
        typeof(IGuestService), typeof(GuestService));
    
    builder.Services.AddScoped(
        typeof(IUserService), typeof(UserService));
    
    builder.Services.AddScoped(
        typeof(IPlagiarismCheckService), 
        typeof(PlagiarismCheckService));
    
    builder.Services.AddScoped(
        typeof(IAppPlagiarismCheckService), 
        typeof(AppPlagiarismCheckService));
    
    builder.Services.AddScoped(
        typeof(IPlagiarismCheckAPI), 
        typeof(PlagiarismCheckAPI));
    
    builder.Services.AddScoped(
        typeof(IAppRephraseService), 
        typeof(AppRephraseService));
    
    builder.Services.AddScoped(
        typeof(IRephraseService), 
        typeof(RephraseService));
}

void ApplyMigrations()
{
    app.Logger.LogInformation("Applying migrations...");
    
    try
    {
        var userDb = scopedProvider.GetRequiredService<UserDataContext>();
        userDb.Database.Migrate();
        
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
            GetRequiredService<UserManager<AppUser>>();
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
