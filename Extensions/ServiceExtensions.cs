
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
}