using System.Reflection;
using System.Text.Json;
using IdentityServer.Application;
using IdentityServer.Persistence;
using IdentityServer.Api.Middleware.SAAuthentication;
using IdentityServer.Application.Common.Configurations;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Inject controllers, and configure JSON serialization to camelCase 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // for JsonSerialize
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // for auto generated json answers such as UnprocessableEntity(ModelState)
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    });

// Register the IOptions object
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<DefaultRoleConfiguration>(builder.Configuration.GetSection("DefaultRoles"));

// Explicitly register the settings object by delegating to the IOptions object
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<DefaultRoleConfiguration>>().Value);

// Inject other layer DI
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

// Injecting automapper configuration for automapping through IMappable
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IIdentityDbContext).Assembly));
});


// Initialize database
try
{
    var scope = builder.Services.BuildServiceProvider().CreateScope();
    var context = scope.ServiceProvider.GetService<IdentityDbContext>();
    var roleConfiguration = scope.ServiceProvider.GetService<DefaultRoleConfiguration>();
    DbInitializer.Initialize(context, roleConfiguration);
}
catch (Exception ex)
{
    // temporary
    Console.WriteLine(ex);
}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSaAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();