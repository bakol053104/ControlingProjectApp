using Microsoft.EntityFrameworkCore;
using ControlingProjectApp.Entities;

namespace ControlingProjectApp.Data
{
    public class ControlingProjectAppDbContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Project> Projects => Set<Project>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}