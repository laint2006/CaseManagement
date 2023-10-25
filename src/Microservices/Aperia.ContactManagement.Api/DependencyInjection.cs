using Aperia.ContactManagement.Api.BackgroundJobs;
using Aperia.ContactManagement.Api.Persistence;
using Aperia.ContactManagement.Api.Repositories;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Messaging;
using Aperia.Core.Messaging.RabbitMq;
using Aperia.Core.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Quartz;
using AppContext = Aperia.Core.Application.AppContext;

namespace Aperia.ContactManagement.Api;

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
                    Name = configuration["AppSettings:AppName"] ?? "Aperia.ContactManagement.Api"
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
        services.AddDbContext<ContactManagementContext>(options => 
        {
            options.UseSqlServer(configuration["ConnectionStrings:ContactManagement"]);
        })
        .AddSingleton<IOutboxMessageConverter, OutboxMessageConverter>()
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<IContactRepository, ContactRepository>();

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
        services.AddHostedService<WorkQueueConsumer>();

        var intervalInSeconds = configuration["ProcessOutboxMessagesJobSettings:IntervalInSeconds"] ?? "300";
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule =>
                    {
                        schedule.WithIntervalInSeconds(int.Parse(intervalInSeconds))
                            .RepeatForever();
                    }));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

}