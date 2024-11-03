using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
        //TODO
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
}
