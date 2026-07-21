namespace QDRD.Media.CollectionRows.Models;

/// <summary>
/// Represents a custom section displayed inside a Jellyfin collection.
/// </summary>
public class CollectionSection
{
    /// <summary>
    /// Gets or sets the section's displayed name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Jellyfin tag used to assign items to this section.
    /// </summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the section's display order.
    /// </summary>
    public int Order { get; set; }
}
