namespace Aperia.Core.Application;

/// <summary>
/// The Application Context
/// </summary>
/// <seealso cref="Aperia.Core.Application.IAppContext" />
public class AppContext : IAppContext
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; set; } = null!;

}