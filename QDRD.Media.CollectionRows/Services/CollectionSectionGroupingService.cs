using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using QDRD.Media.CollectionRows.Models;

namespace QDRD.Media.CollectionRows.Services;

/// <summary>
/// Groups collection items into display sections using their Jellyfin tags.
/// </summary>
public class CollectionSectionGroupingService
{
    private readonly CollectionSectionMatcher _sectionMatcher;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CollectionSectionGroupingService"/> class.
    /// </summary>
    /// <param name="sectionMatcher">
    /// The service used to match item tags to configured sections.
    /// </param>
    public CollectionSectionGroupingService(
        CollectionSectionMatcher sectionMatcher)
    {
        ArgumentNullException.ThrowIfNull(sectionMatcher);
        _sectionMatcher = sectionMatcher;
    }

    /// <summary>
    /// Groups collection items into the default Movies row and configured custom rows.
    /// </summary>
    /// <param name="items">
    /// A dictionary containing each Jellyfin item's identifier and its tags.
    /// </param>
    /// <param name="sections">The configured custom sections.</param>
    /// <returns>The rows that should be displayed on the collection page.</returns>
    public Collection<CollectionSectionGroup> CreateGroups(
        IReadOnlyDictionary<Guid, IReadOnlyCollection<string>> items,
        IEnumerable<CollectionSection> sections)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(sections);

        List<CollectionSection> configuredSections = sections
            .OrderBy(section => section.Order)
            .ToList();

        CollectionSectionGroup defaultGroup = new()
        {
            Name = "Movies",
            Order = int.MinValue
        };

        Dictionary<CollectionSection, CollectionSectionGroup> customGroups = [];

        foreach (CollectionSection section in configuredSections)
        {
            customGroups[section] = new CollectionSectionGroup
            {
                Name = section.Name,
                Order = section.Order
            };
        }

        foreach (KeyValuePair<Guid, IReadOnlyCollection<string>> item in items)
        {
            CollectionSection? matchingSection =
                _sectionMatcher.FindSection(item.Value, configuredSections);

            if (matchingSection is null)
            {
                defaultGroup.ItemIds.Add(item.Key);
                continue;
            }

            customGroups[matchingSection].ItemIds.Add(item.Key);
        }

        Collection<CollectionSectionGroup> groups = [];

        if (defaultGroup.ItemIds.Count > 0)
        {
            groups.Add(defaultGroup);
        }

        foreach (CollectionSectionGroup group in customGroups.Values
                     .Where(group => group.ItemIds.Count > 0)
                     .OrderBy(group => group.Order))
        {
            groups.Add(group);
        }

        return groups;
    }
}
