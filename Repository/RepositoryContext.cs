using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
        //TODO
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ComapnyConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
}
