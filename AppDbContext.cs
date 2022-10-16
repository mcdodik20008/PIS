using Microsoft.EntityFrameworkCore;

namespace PISWF
{
    // Создать dotnet ef dbcontext scaffold "Server=localhost;Database=master;Trusted_Connection=True;"  Microsoft.EntityFrameworkCore.SqlServer -c ApplicationContext
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              optionsBuilder.UseNpgsql("Server=localhost:5432;Database=pis;User Id=postgres;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
