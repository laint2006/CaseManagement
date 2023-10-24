using Aperia.ContactManagement.Api.BackgroundJobs;
using Aperia.ContactManagement.Api.Persistence;
using Aperia.ContactManagement.Api.Repositories;
using Aperia.Core.Application.Behaviors;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Messaging;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Reflection;
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
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddSingleton<IJsonSerializer, JsonSerializer>()
                .AddScoped<IAppContext>(sp=> new AppContext
                {
                    Name = configuration["AppSettings:AppName"] ?? "Aperia.ContactManagement.Api"
                })
                .AddMediatR(options =>
                {
                    options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                })
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
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
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

}