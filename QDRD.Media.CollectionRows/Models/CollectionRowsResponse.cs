using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QDRD.Media.CollectionRows.Models;

/// <summary>
/// Describes the grouped rows for one Jellyfin collection.
/// </summary>
public class CollectionRowsResponse
{
    /// <summary>
    /// Gets or sets the Jellyfin identifier of the collection.
    /// </summary>
    public Guid CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the collection's display name.
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the rows that should be displayed for the collection.
    /// </summary>
    public Collection<CollectionRowResponse> Rows { get; } = [];
}

/// <summary>
/// Describes one visible row inside a collection.
/// </summary>
public class CollectionRowResponse
{
    /// <summary>
    /// Gets or sets the row's display name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the row's display order.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Gets the Jellyfin identifiers of the items in this row.
    /// </summary>
    public Collection<Guid> ItemIds { get; } = [];
}
