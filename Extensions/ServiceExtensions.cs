
public static class ServiceExtensions
{
    public static void ConfigureUserRepository(this IServiceCollection services)
    {
        services.AddScoped<IUSerRepository, UserRepository>();
    }
}