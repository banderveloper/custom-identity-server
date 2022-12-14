using System.Reflection;
using IdentityServer.Application;
using IdentityServer.Persistence;
using IdentityServer.Api.Middleware.SAAuthentication;
using IdentityServer.Application.Common.Mappings;
using IdentityServer.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Inject other layer DI
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers();

// Injecting automapper configuration for automapping through IMappable<>
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IIdentityDbContext).Assembly));
});

// Initialize db
try
{
    var scope = builder.Services.BuildServiceProvider().CreateScope();
    var context = scope.ServiceProvider.GetService<IdentityDbContext>();
    DbInitializer.Initialize(context, builder.Configuration);
}
catch (Exception ex)
{
    // temporary
    Console.WriteLine(ex);
}

var app = builder.Build();

app.UseSaAuthentication();
app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();