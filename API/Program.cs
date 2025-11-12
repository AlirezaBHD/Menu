using API.Configurations;
using Application;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
builder.Host.UseAppLogging(builder.Configuration);

services
    .AddAppCors(configuration)
    .AddAppControllers()
    .AddAppDbContext(configuration)
    .AddAppRateLimiting()
    .AddAppSession()
    .AddAppLocalization()
    .AddSwaggerDocumentation(configuration)
    .AddApplicationServices()
    .AddApplicationRepositories();

services.AddAuthorization();
services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwaggerDocumentation();
}

app.MapGet("/health", () => Results.Ok("I'm alive"));

app.ConfigureApplicationLanguage();
app.MapControllers();
app.UseAppMiddlewares();

app.Run();