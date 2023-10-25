using Aperia.Core.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aperia.Core.Application;

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

    /// <summary>
    /// Adds the mediator.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(options =>
                {
                    options.RegisterServicesFromAssembly(assembly);
                })
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    /// <summary>
    /// Adds the fluent validation.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    public static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }

}