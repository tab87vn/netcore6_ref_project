namespace tab.TestDotNet.API.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using tab.TestDotNet.API.Models;
using tab.TestDotNet.API.Repositories;
using tab.TestDotNet.Services.Contracts;
using tab.TestDotNet.Services.LoggerServices;

public static class ServiceExtensions
{
    public static void ConfigureUserRepository(this IServiceCollection services)
    {
        services.AddScoped<IUSerRepository, UserRepository>();
    }

    public static void ConfigureCors(
        this IServiceCollection services,
        string policyName)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(policyName, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
    }

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureSqlContext(
        this IServiceCollection services,
        IConfiguration configuration)
        {

            services.AddDbContext<TestAppDbContext>(opts =>
            opts.UseSqlite(configuration.GetConnectionString("sqlConnection")));
        }
}