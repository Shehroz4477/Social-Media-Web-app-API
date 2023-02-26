using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using serverSite.Data;
using serverSite.Interfaces;
using serverSite.Services;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>((DbContextOptionsBuilder context) => context.UseSqlite(builder.Configuration.GetConnectionString("socialMediaSqliteConnection")));
builder.Services.AddCors();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer((JwtBearerOptions jwtBearerOptions) => 
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

//  Configure the HTTP request pipeline
app.UseCors(configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
