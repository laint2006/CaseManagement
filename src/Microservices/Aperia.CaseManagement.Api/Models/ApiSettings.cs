namespace Aperia.CaseManagement.Api.Models;

/// <summary>
/// The API Settings
/// </summary>
public sealed class ApiSettings
{
    /// <summary>
    /// The configuration section
    /// </summary>
    public const string ConfigurationSection = "ApiSettings";

    /// <summary>
    /// Gets or sets the acu API.
    /// </summary>
    public string? AcuApi { get; set; }

    /// <summary>
    /// Gets or sets the contact management API.
    /// </summary>
    public string? ContactManagementApi { get; set; }

    /// <summary>
    /// Gets or sets the ownership API.
    /// </summary>
    public string? OwnershipApi { get; set; }

    /// <summary>
    /// Gets or sets the sla ola API.
    /// </summary>
    public string? SlaOlaApi { get; set; }

}