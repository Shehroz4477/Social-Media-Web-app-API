using Microsoft.EntityFrameworkCore;
using serverSite.Data;
using serverSite.Extensions;
using serverSite.Middleware;
var policyName = "_developmentPolicy";
var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddCors(options => options.AddPolicy(name: policyName,policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

//  Configure the HTTP request pipeline
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(policyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUserData(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An error occurred during migration");
}

app.Run();
