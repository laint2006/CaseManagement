using Aperia.Acu.Api.Persistence;
using Aperia.Acu.Api.Repositories;
using Aperia.Core.Application.Behaviors;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AppContext = Aperia.Core.Application.AppContext;

namespace Aperia.Acu.Api;

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
                    Name = configuration["AppSettings:AppName"] ?? "Aperia.Acu.Api"
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
        services.AddDbContext<AcuContext>(options => 
        {
            options.UseSqlServer(configuration["ConnectionStrings:Acu"]);
        })
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<ITriggerRepository, TriggerRepository>();

        return services;
    }

}