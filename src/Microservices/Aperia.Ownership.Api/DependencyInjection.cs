using Aperia.Core.Application.Behaviors;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Persistence.Converters;
using Aperia.Ownership.Api.Persistence;
using Aperia.Ownership.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AppContext = Aperia.Core.Application.AppContext;

namespace Aperia.Ownership.Api;

/// <summary>
/// The Dependency Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the application.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IJsonSerializer, JsonSerializer>()
                .AddScoped<IAppContext>(sp=> new AppContext
                {
                    Name = configuration["AppSettings:AppName"] ?? "Aperia.Ownership.Api"
                })
                .AddMediatR(options =>
                {
                    options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                })
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    /// <summary>
    /// Adds the persistence.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OwnershipContext>(options => 
        {
            options.UseSqlServer(configuration["ConnectionStrings:Ownership"]);
        })
        .AddSingleton<IOutboxMessageConverter, OutboxMessageConverter>()
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<IOwnerRepository, OwnerRepository>();

        return services;
    }

}