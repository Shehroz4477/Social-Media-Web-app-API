using serverSite.Extensions;
using serverSite.Middleware;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

//  Configure the HTTP request pipeline
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
