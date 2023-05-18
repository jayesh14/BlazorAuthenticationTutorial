using BlazorAuthenticationTutorial.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAuthenticationTutorial.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(
                 new UserRole { Id = 1, Name = "Admin" },
                 new UserRole { Id = 2, Name = "User" }
             );
        }

        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
