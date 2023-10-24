using Microsoft.Extensions.DependencyInjection;

namespace Aperia.Core.Persistence;

/// <summary>
/// The Dependency Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the application context.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddAppContext<TContext>(this IServiceCollection services)
        where TContext : class, IAppContext
    {
        services.AddScoped<IAppContext, TContext>();

        return services;
    }
}