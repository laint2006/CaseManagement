using Aperia.Core.Messaging.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aperia.Core.Messaging;

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
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<RabbitMqSettings>()
            .BindConfiguration(RabbitMqSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IRabbitMqConnectionManager>(sp =>
        {
            var rabbitMqSettings = sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
            var factory = new ConnectionFactory
            {
                HostName = rabbitMqSettings.HostName,
                VirtualHost = rabbitMqSettings.VirtualHost,
                Port = rabbitMqSettings.Port,
                UserName = rabbitMqSettings.UserName,
                Password = rabbitMqSettings.Password,
                DispatchConsumersAsync = rabbitMqSettings.UseAsyncDispatchConsumer
            };

            var logger = sp.GetRequiredService<ILogger<RabbitMqConnectionManager>>();

            return new RabbitMqConnectionManager(factory, logger, rabbitMqSettings.RetryCount);
        });

        return services;
    }

}