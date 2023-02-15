using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext>((DbContextOptionsBuilder context) => context.UseSqlite(builder.Configuration.GetConnectionString("socialMediaSqliteConnection")));

var app = builder.Build();

app.MapControllers();

app.Run();
