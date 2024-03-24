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
            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb;Database=ControlingProject");
        }
      
    }
}