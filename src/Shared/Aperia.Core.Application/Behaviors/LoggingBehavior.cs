using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Aperia.Core.Application.Behaviors;

/// <summary>
/// The Logging Behavior
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IPipelineBehavior&lt;TRequest, TResponse&gt;" />
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        this._logger = logger;
    }

    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Awaitable task returning the <typeparamref name="TResponse" />
    /// </returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? response;
        var stopwatch = Stopwatch.StartNew();
        var requestId = Guid.NewGuid();

        try
        {
            response = await next();
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An error occurred");
            throw;
        }
        finally
        {
            stopwatch.Stop();

            _logger.LogInformation($"Request ID: {requestId}. Duration: {stopwatch.ElapsedMilliseconds:#,###}ms");
        }

        return response;
    }
}