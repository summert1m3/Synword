using Application.AutoMapper;
using Application.Guests.Services;
using Application.PlagiarismCheck.Services;
using Application.Users.Services;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint.Extensions;
using Synword.Domain.Interfaces;
using Synword.Infrastructure.Data;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;
using MediatR;
using Synword.Domain.Services.PlagiarismCheck;
using Synword.Infrastructure.Services.PlagiarismCheckAPI;
using Synword.PublicApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

Synword.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllers();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwagger();

builder.Services.AddJwtBearerAuthentication(builder.Configuration);

AddUserServices();

var app = builder.Build();

app.Logger.LogInformation("PublicApi App created...");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();

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
