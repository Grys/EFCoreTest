using Microsoft.EntityFrameworkCore;

namespace RuCitizens.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Citizen> Citizens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>().ToTable("Citizens");
        }

    }
}
