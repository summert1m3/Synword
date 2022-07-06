using System.Text;
using Application.Documents.Services;
using Application.EnhancedRephrase.Services;
using Application.Guests.Services;
using Application.PlagiarismCheck.Services;
using Application.Rephrase;
using Application.Rephrase.Services;
using Application.Token.Services;
using Application.Users.Services;
using Application.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Synword.Domain.Interfaces.Repository;
using Synword.Domain.Interfaces.Services;
using Synword.Domain.Services.EnhancedRephrase;
using Synword.Domain.Services.PlagiarismCheck;
using Synword.Domain.Services.Rephrase;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services;
using Synword.Infrastructure.Services.Docx;
using Synword.Infrastructure.Services.Google;
using Synword.Infrastructure.Services.PlagiarismCheckAPI;
using Synword.Infrastructure.SynonymDictionary.EngSynonymDictionary;
using Synword.Infrastructure.SynonymDictionary.RusSynonymDictionary;
using Synword.Infrastructure.Synword;

namespace Synword.PublicApi;

public static class ServiceExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
        
        var key = Encoding.ASCII.GetBytes(configuration["JWT_SECRET_KEY"]);
        services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
        services.AddAuthorization();
        
        services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();
        
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Synword API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
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
        
        return services;
    }
    
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped(
            typeof(IGoogleApi), typeof(GoogleApi));
    
        services.AddScoped(
            typeof(IGuestService), typeof(GuestService));
    
        services.AddScoped(
            typeof(IUserService), typeof(UserService));
    
        services.AddScoped(
            typeof(IPlagiarismCheckService), 
            typeof(PlagiarismCheckService));
    
        services.AddScoped(
            typeof(IAppPlagiarismCheckService), 
            typeof(AppPlagiarismCheckService));
    
        services.AddScoped(
            typeof(IPlagiarismCheckAPI), 
            typeof(PlagiarismCheckAPI));
    
        services.AddScoped(
            typeof(IAppRephraseService), 
            typeof(AppRephraseService));
    
        services.AddScoped(
            typeof(IRephraseService), 
            typeof(RephraseService));
        
        services.AddScoped(
            typeof(IAppDocxService), 
            typeof(AppDocxService));

        services.AddScoped(
            typeof(IDocxService), 
            typeof(DocxService));
        
        services.AddScoped(
            typeof(IRephraseRequestValidation), 
            typeof(RephraseRequestValidation));
        
        services.AddScoped(
            typeof(IPlagiarismRequestValidation), 
            typeof(PlagiarismRequestValidation));
        
        services.AddScoped(
            typeof(IAppEnhancedRephraseService), 
            typeof(AppEnhancedRephraseService));
        
        services.AddScoped(
            typeof(IEnhancedRephraseService), 
            typeof(EnhancedRephraseService));
        
        services.AddScoped(
            typeof(IYandexTranslateApi), 
            typeof(YandexTranslateApi));
        
        services.AddScoped(
            typeof(IEnhancedRephraseRequestValidation), 
            typeof(EnhancedRephraseRequestValidation));
        services.AddScoped(
            typeof(IAppTokenService), 
            typeof(AppTokenService));
        
        
        return services;
    }
}
