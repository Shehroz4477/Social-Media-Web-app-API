using Microsoft.EntityFrameworkCore;
using serverSite.Data;
using serverSite.Interfaces;
using serverSite.Services;

namespace serverSite.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>((DbContextOptionsBuilder context) => context.UseSqlite(config.GetConnectionString("socialMediaSqliteConnection")));
            services.AddCors();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserRepository,UserRepository>();

            return services;
        }
    }
}