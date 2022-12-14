using IdentityServer.Application;
using IdentityServer.Persistence;
using IdentityServer.Api.Middleware.SAAuthentication;

var builder = WebApplication.CreateBuilder(args);

// Inject other layer DI
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers();

// Initialize db
try
{
    var scope = builder.Services.BuildServiceProvider().CreateScope();
    var context = scope.ServiceProvider.GetService<IdentityDbContext>();
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    // temporary
    Console.WriteLine(ex);
}

var app = builder.Build();

app.UseSaAuthentication();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();