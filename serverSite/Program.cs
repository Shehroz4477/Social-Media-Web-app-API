using Microsoft.EntityFrameworkCore;
using serverSite.Data;
using serverSite.Interfaces;
using serverSite.Services;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>((DbContextOptionsBuilder context) => context.UseSqlite(builder.Configuration.GetConnectionString("socialMediaSqliteConnection")));
builder.Services.AddCors();
builder.Services.AddScoped<ITokenService,TokenService>();

var app = builder.Build();

//  Configure the HTTP request pipeline
app.UseCors(configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.MapControllers();

app.Run();
