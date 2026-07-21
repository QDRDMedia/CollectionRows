using System.Collections.ObjectModel;
using QDRD.Media.CollectionRows.Models;
using MediaBrowser.Model.Plugins;

namespace QDRD.Media.CollectionRows.Configuration;

/// <summary>
/// Configuration settings for the Collection Organizer plugin.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Gets the custom sections displayed inside collections.
    /// </summary>
    public Collection<CollectionSection> Sections { get; } = [];
}
