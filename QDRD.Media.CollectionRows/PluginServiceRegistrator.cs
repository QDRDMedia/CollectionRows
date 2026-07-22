using MediaBrowser.Controller;
using MediaBrowser.Controller.Plugins;
using Microsoft.Extensions.DependencyInjection;
using QDRD.Media.CollectionRows.Services;

namespace QDRD.Media.CollectionRows
{
    /// <summary>
    /// Registers the Collection Rows services with Jellyfin.
    /// </summary>
    public class PluginServiceRegistrator : IPluginServiceRegistrator
    {
        /// <inheritdoc />
        public void RegisterServices(
            IServiceCollection serviceCollection,
            IServerApplicationHost applicationHost)
        {
            serviceCollection.AddSingleton<CollectionSectionMatcher>();
            serviceCollection.AddSingleton<CollectionSectionGroupingService>();
        }
    }
}
