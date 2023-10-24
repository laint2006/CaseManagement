using System.Globalization;

namespace Aperia.Core.Presentation.Middlewares;

/// <summary>
/// The Application Middleware
/// </summary>
public class ApplicationMiddleware
{
    /// <summary>
    /// The next
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// The random
    /// </summary>
    private static readonly Random Random = new Random();

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    public ApplicationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the asynchronous.
    /// </summary>
    /// <param name="context">The context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var cultureQuery = context.Request.Query["culture"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            var culture = new CultureInfo(cultureQuery);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

#if DEBUG2
        // Call the next delegate/middleware in the pipeline.
        var number = Random.Next(1, 10);
        if (number % 4 == 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
#endif

        await _next(context);
    }

}