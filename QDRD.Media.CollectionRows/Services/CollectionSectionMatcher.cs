using System;
using System.Collections.Generic;
using System.Linq;
using QDRD.Media.CollectionRows.Models;

namespace QDRD.Media.CollectionRows.Services;

/// <summary>
/// Matches media items to custom collection sections using Jellyfin tags.
/// </summary>
public class CollectionSectionMatcher
{
    /// <summary>
    /// Finds the first configured section matching one of the item's tags.
    /// </summary>
    /// <param name="itemTags">The tags assigned to the media item.</param>
    /// <param name="sections">The configured collection sections.</param>
    /// <returns>
    /// The matching collection section, or null when the item belongs in the
    /// default Movies section.
    /// </returns>
    public CollectionSection? FindSection(
        IEnumerable<string> itemTags,
        IEnumerable<CollectionSection> sections)
    {
        ArgumentNullException.ThrowIfNull(itemTags);
        ArgumentNullException.ThrowIfNull(sections);

        HashSet<string> tags = new(
            itemTags.Where(tag => !string.IsNullOrWhiteSpace(tag)),
            StringComparer.OrdinalIgnoreCase);

        return sections
            .Where(section =>
                !string.IsNullOrWhiteSpace(section.Tag) &&
                tags.Contains(section.Tag))
            .OrderBy(section => section.Order)
            .FirstOrDefault();
    }
}
