using Aperia.Acu.Api.BackgroundJobs;
using Aperia.Acu.Api.Models;
using Aperia.Acu.Api.Persistence;
using Aperia.Acu.Api.Repositories;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Messaging;
using Aperia.Core.Messaging.RabbitMq;
using Aperia.Core.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
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
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IJsonSerializer, JsonSerializer>()
                .AddScoped<IAppContext>(sp=> new AppContext
                {
                    Name = configuration["AppSettings:AppName"] ?? "Aperia.Acu.Api"
                })
                .AddMediator(assembly)
                .AddFluentValidation(assembly)
                .AddBackgroundJobs(configuration)
                .AddRabbitMq(configuration);

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
        .AddSingleton<IOutboxMessageConverter, OutboxMessageConverter>()
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<ITriggerRepository, TriggerRepository>();

        return services;
    }

    /// <summary>
    /// Adds the background jobs.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<RabbitMqPublisherSettings>()
            .BindConfiguration("RabbitMqEventPublisherSettings")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<RabbitMqConsumerSettings>()
            .BindConfiguration("RabbitMqEventConsumerSettings")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IEventPublisher, RabbitMqPublisher>();
        services.AddHostedService<ProcessMessageBackgroundJob>();

        return services;
    }

}