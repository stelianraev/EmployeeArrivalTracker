namespace EmployeeArrivalTracker.Data
{
    using EmployeeArrivalTracker.Configuration;
    using global::EmployeeArrivalTracker.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeArrivalTrackerDbContext : DbContext
    {
        public EmployeeArrivalTrackerDbContext(DbContextOptions<EmployeeArrivalTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; init; }
        public DbSet<Team> Teams { get; init; }
        public DbSet<ArrivalInformation> ArrivalInformation { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Employee>()
                .HasMany(x => x.ArrivalInformation)
                .WithOne(x => x.Employee)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Team>()
                .HasMany(x => x.Employees)
                .WithMany(x => x.Teams);

            modelBuilder.Entity<Employee>()
            .HasMany<Team>(t => t.Teams)
            .WithMany(e => e.Employees);

            base.OnModelCreating(modelBuilder);
        }
    }
}
