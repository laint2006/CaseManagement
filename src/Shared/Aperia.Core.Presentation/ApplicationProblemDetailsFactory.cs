using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Aperia.Core.Presentation;

/// <summary>
/// The Application Problem Details Factory
/// </summary>
/// <seealso cref="ProblemDetailsFactory" />
public class ApplicationProblemDetailsFactory : ProblemDetailsFactory
{
    /// <summary>
    /// The options
    /// </summary>
    private readonly ApiBehaviorOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationProblemDetailsFactory"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public ApplicationProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ProblemDetails" /> instance that configures defaults based on values specified in <see cref="T:Microsoft.AspNetCore.Mvc.ApiBehaviorOptions" />.
    /// </summary>
    /// <param name="httpContext">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" />.</param>
    /// <param name="statusCode">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Status" />.</param>
    /// <param name="title">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Title" />.</param>
    /// <param name="type">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.TransactionType" />.</param>
    /// <param name="detail">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Detail" />.</param>
    /// <param name="instance">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Instance" />.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.AspNetCore.Mvc.ProblemDetails" /> instance.
    /// </returns>
    public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ValidationProblemDetails" /> instance that configures defaults based on values specified in <see cref="T:Microsoft.AspNetCore.Mvc.ApiBehaviorOptions" />.
    /// </summary>
    /// <param name="httpContext">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" />.</param>
    /// <param name="modelStateDictionary">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" />.</param>
    /// <param name="statusCode">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Status" />.</param>
    /// <param name="title">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Title" />.</param>
    /// <param name="type">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.TransactionType" />.</param>
    /// <param name="detail">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Detail" />.</param>
    /// <param name="instance">The value for <see cref="P:Microsoft.AspNetCore.Mvc.ProblemDetails.Instance" />.</param>
    /// <returns>
    /// The <see cref="T:Microsoft.AspNetCore.Mvc.ValidationProblemDetails" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">modelStateDictionary</exception>
    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Applies the problem details defaults.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="problemDetails">The problem details.</param>
    /// <param name="statusCode">The status code.</param>
    private void ApplyProblemDetailsDefaults(HttpContext? httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        if (httpContext?.Items[HttpContextItemKeys.Errors] is List<Error> errors)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }
    }

}