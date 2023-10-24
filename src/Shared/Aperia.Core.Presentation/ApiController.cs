namespace Aperia.Core.Presentation;

/// <summary>
/// The API Controller
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// Creates the Problem result from the given error.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns></returns>
    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return this.Problem(statusCode: statusCode, title: error.Description);
    }

    /// <summary>
    /// Creates the Problem result from the given errors.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns></returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
        {
            return this.Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return this.ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return this.Problem(errors[0]);
    }

    /// <summary>
    /// Validations the problem.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns></returns>
    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }

}