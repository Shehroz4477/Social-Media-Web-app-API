using Microsoft.EntityFrameworkCore;
using serverSite.Data;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>((DbContextOptionsBuilder context) => context.UseSqlite(builder.Configuration.GetConnectionString("socialMediaSqliteConnection")));
builder.Services.AddCors();

var app = builder.Build();

//  Configure the HTTP request pipeline
app.UseCors(configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
app.MapControllers();

app.Run();
