using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using tab.TestDotNet.API.Models;

namespace tab.TestDotNet.API.Repositories;

public class DbContextFactory : IDesignTimeDbContextFactory<TestAppDbContext>
{
    public TestAppDbContext CreateDbContext(string[] args)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = System.IO.Path.Join(path, "blogging.db");
        
        var builder = new DbContextOptionsBuilder<TestAppDbContext>()
            .UseSqlite($"Data Source={dbPath}", 
            b => b.MigrationsAssembly("CompaniesEmployees"));
        
        return new TestAppDbContext(builder.Options);
    }
}