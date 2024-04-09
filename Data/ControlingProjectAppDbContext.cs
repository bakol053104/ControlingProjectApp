using ControlingProjectApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlingProjectApp.Data;

public class ControlingProjectAppDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Project> Projects => Set<Project>();

    public ControlingProjectAppDbContext(DbContextOptions<ControlingProjectAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb;Database=ControlingProject;
                                        Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");
    }
}

