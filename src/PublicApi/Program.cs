using Microsoft.AspNetCore.Identity;
using MinimalApi.Endpoint.Extensions;
using Synword.ApplicationCore.Interfaces;
using Synword.Infrastructure.Data;
using Synword.Infrastructure.Identity;
using Synword.Infrastructure.Services.Google;
using Synword.PublicApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints();

Synword.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllers();

builder.Services.AddSwagger();

builder.Services.AddJwtBearerAuthentication(builder.Configuration);

builder.Services.AddScoped(typeof(IRepository<>), typeof(UserDataRepository<>));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped(typeof(IGoogleApi), typeof(GoogleApi));

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
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.MapEndpoints();
app.Logger.LogInformation("LAUNCHING PublicApi");
app.Run();
