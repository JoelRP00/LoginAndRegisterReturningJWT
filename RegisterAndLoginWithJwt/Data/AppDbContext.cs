using Microsoft.EntityFrameworkCore;
using RegisterAndLoginWithJwt.Model;

namespace RegisterAndLoginWithJwt.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions options, IConfiguration configuration ) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Connection"));
        }
    }
}
