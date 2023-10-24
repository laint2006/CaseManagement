using Aperia.Core.Presentation.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace Aperia.Core.Presentation;

/// <summary>
/// The Dependency Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the presentation.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        services.AddSingleton<ProblemDetailsFactory, ApplicationProblemDetailsFactory>();

        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.AddFixedWindowLimiter("api_rate_limit_policy", options =>
            {
                options.PermitLimit = 2;
                options.Window = TimeSpan.FromSeconds(50);
                //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                //options.QueueLimit = 2;
            });
        });

        return services;
    }

    /// <summary>
    /// Adds the middlewares.
    /// </summary>
    /// <param name="applicationBuilder">The application builder.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ApplicationMiddleware>();

        return applicationBuilder;
    }
}