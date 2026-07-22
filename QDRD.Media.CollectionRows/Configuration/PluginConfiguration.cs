using System.Collections.ObjectModel;
using MediaBrowser.Model.Plugins;
using QDRD.Media.CollectionRows.Models;

namespace QDRD.Media.CollectionRows.Configuration;

/// <summary>
/// Configuration settings for the Collection Rows plugin.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        Sections =
        [
            new CollectionSection
            {
                Name = "Documentaries",
                Tag = "CollectionSection:Documentary",
                Order = 10
            },
            new CollectionSection
            {
                Name = "Fan Films",
                Tag = "CollectionSection:Fan Film",
                Order = 20
            }
        ];
    }

    /// <summary>
    /// Gets or sets the custom sections displayed inside collections.
    /// </summary>
    public Collection<CollectionSection> Sections { get; set; }
}
