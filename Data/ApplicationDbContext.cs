using Microsoft.EntityFrameworkCore;
using EMA.Models.Entities;

namespace EMA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore `WeatherForecast` so it's not tracked or included in migrations
            modelBuilder.Ignore<WeatherForecast>();

            // Configure Employee precision and gender
            modelBuilder.Entity<Models.Entities.Employee>(eb =>
            {
                eb.Property(e => e.Salary).HasPrecision(18, 2);
                eb.Property(e => e.Gender).HasMaxLength(1).IsRequired();
            });
        }
    }
}
