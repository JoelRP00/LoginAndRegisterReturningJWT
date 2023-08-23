using Microsoft.EntityFrameworkCore;
using RegisterAndLoginWithJwt.Data;
using RegisterAndLoginWithJwt.Repositories;
using RegisterAndLoginWithJwt.Repositories.Interface;
using RegisterAndLoginWithJwt.Services;
using RegisterAndLoginWithJwt.Services.Interface;

namespace RegisterAndLoginWithJwt.Dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Connection")));
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddServices (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserServices, UserServices>();
            return services;
        }
    }
}
