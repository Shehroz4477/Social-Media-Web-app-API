using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext>((DbContextOptionsBuilder context) => context.UseSqlite(builder.Configuration.GetConnectionString("socialMediaSqliteConnection")));

var app = builder.Build();

//  Configure the HTTP request pipeline
app.MapControllers();

app.Run();
