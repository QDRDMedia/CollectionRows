using System;
using System.Collections.ObjectModel;

namespace QDRD.Media.CollectionRows.Models;

/// <summary>
/// Represents one displayed row of items inside a Jellyfin collection.
/// </summary>
public class CollectionSectionGroup
{
    /// <summary>
    /// Gets or sets the heading displayed above the row.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the row's display order.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Gets the Jellyfin item identifiers displayed in this row.
    /// </summary>
    public Collection<Guid> ItemIds { get; } = [];
}
