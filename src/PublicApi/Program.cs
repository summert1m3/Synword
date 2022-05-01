﻿using Application.AutoMapper;
using Application.Guests.Services;
using Application.PlagiarismCheck.Services;
using Application.Users.Services;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint.Extensions;
using Synword.Domain.Interfaces;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Synword.Domain.Services.PlagiarismCheck;
using Synword.Infrastructure.Services.PlagiarismCheckAPI;
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

AddUserServices();

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

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        Console.WriteLine("Applying migrations...");
        var userDb = scopedProvider.GetRequiredService<UserDataContext>();
        userDb.Database.Migrate();
        
        var identityDb = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        identityDb.Database.Migrate();

        Console.WriteLine("Migration completed.");
        
        var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB");
    }
}

app.MapEndpoints();
app.Logger.LogInformation("LAUNCHING PublicApi");
app.Run();


void AddUserServices()
{
    builder.Services.AddScoped(
        typeof(IRepository<>), typeof(UserDataRepository<>));
    builder.Services.AddSingleton(builder.Configuration);
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
    builder.Services.AddAutoMapper(typeof(DomainProfile));
}
