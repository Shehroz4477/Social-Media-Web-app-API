using Microsoft.EntityFrameworkCore;
using serverSite.Data;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>((DbContextOptionsBuilder context) => context.UseSqlite(builder.Configuration.GetConnectionString("socialMediaSqliteConnection")));

var app = builder.Build();

//  Configure the HTTP request pipeline
app.MapControllers();

app.Run();
