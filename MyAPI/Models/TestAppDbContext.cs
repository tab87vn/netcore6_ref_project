using Microsoft.EntityFrameworkCore;

namespace tab.TestDotNet.API.Models;

public class TestAppDbContext : DbContext
{
    public TestAppDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Company>? Companies { get; set; }
    public DbSet<Employee>? Employees { get; set; }

}