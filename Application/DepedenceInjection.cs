public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBotService, BotService>();
        services.AddScoped<IGeminiService, GeminiService>();
        return services;
    }
}