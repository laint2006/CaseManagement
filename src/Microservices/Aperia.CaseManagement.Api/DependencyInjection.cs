using Aperia.CaseManagement.Api.BackgroundJobs;
using Aperia.CaseManagement.Api.Models;
using Aperia.CaseManagement.Api.Persistence;
using Aperia.CaseManagement.Api.Repositories;
using Aperia.CaseManagement.Api.Services;
using Aperia.Core.Application.Repositories;
using Aperia.Core.Application.Services;
using Aperia.Core.Messaging;
using Aperia.Core.Messaging.RabbitMq;
using Aperia.Core.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quartz;
using AppContext = Aperia.Core.Application.AppContext;

namespace Aperia.CaseManagement.Api;

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
            .AddScoped<IAppContext>(sp => new AppContext
            {
                Name = configuration["AppSettings:AppName"] ?? "Aperia.CaseManagement.Api"
            })
            .AddMediator(assembly)
            .AddFluentValidation(assembly)
            .AddHttpClients(configuration)
            .AddServices(configuration)
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
        services.AddDbContext<CaseManagementContext>(options => 
        {
            options.UseSqlServer(configuration["ConnectionStrings:CaseManagement"]);
        })
        .AddSingleton<IOutboxMessageConverter, OutboxMessageConverter>()
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped<IInquiryRepository, InquiryRepository>();

        return services;
    }

    /// <summary>
    /// Adds the services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAcuService, AcuService>()
            .AddTransient<IOwnershipService, OwnershipService>()
            .AddTransient<IContactService, ContactService>()
            .AddTransient<ISlaOlaService, SlaOlaService>();

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

    /// <summary>
    /// Adds the services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ApiSettings>()
            .BindConfiguration(ApiSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient("acu", (sp, httpClient) =>
        {
            var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
            SetupHttpClient(httpClient, apiSettings, apiSettings.AcuApi);
        });

        services.AddHttpClient("contactManagement", (sp, httpClient) =>
        {
            var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
            SetupHttpClient(httpClient, apiSettings, apiSettings.ContactManagementApi);
        });

        services.AddHttpClient("ownership", (sp, httpClient) =>
        {
            var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
            SetupHttpClient(httpClient, apiSettings, apiSettings.OwnershipApi);
        });

        services.AddHttpClient("slaola", (sp, httpClient) =>
        {
            var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
            SetupHttpClient(httpClient, apiSettings, apiSettings.SlaOlaApi);
        });

        return services;
    }

    /// <summary>
    /// Setups the HTTP client.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="apiSettings">The API settings.</param>
    /// <param name="baseAddress">The base address.</param>
    /// <exception cref="System.ArgumentNullException">baseAddress</exception>
    private static void SetupHttpClient(HttpClient httpClient, ApiSettings apiSettings, string? baseAddress)
    {
        if (string.IsNullOrWhiteSpace(baseAddress))
        {
            throw  new ArgumentNullException(nameof(baseAddress));
        }

        httpClient.DefaultRequestHeaders.Add("User-Agent", "Aperia");
        httpClient.DefaultRequestHeaders.Add("X-RequestId", Guid.NewGuid().ToString());
        httpClient.Timeout = TimeSpan.FromSeconds(apiSettings.DefaultTimeoutInSeconds);
        httpClient.BaseAddress = new Uri(baseAddress);
    }

}