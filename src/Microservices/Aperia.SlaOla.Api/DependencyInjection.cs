using Aperia.Core.Application.Behaviors;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Persistence.Converters;
using Aperia.SlaOla.Api.Persistence;
using Aperia.SlaOla.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AppContext = Aperia.Core.Application.AppContext;

namespace Aperia.SlaOla.Api;

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
                .AddSingleton<IAppContext>(sp=> new AppContext
                {
                    Name = configuration["AppSettings:AppName"] ?? "Aperia.SlaOla.Api"
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
        services.AddDbContext<SlaOlaContext>(options => 
        {
            options.UseSqlServer(configuration["ConnectionStrings:SlaOla"]);
        })
        .AddSingleton<IOutboxMessageConverter, OutboxMessageConverter>()
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<ILaRepository, LaRepository>();

        return services;
    }

}