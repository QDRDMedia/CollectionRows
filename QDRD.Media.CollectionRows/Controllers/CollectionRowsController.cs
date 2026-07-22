using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QDRD.Media.CollectionRows.Models;
using QDRD.Media.CollectionRows.Services;

namespace QDRD.Media.CollectionRows.Controllers;

/// <summary>
/// Provides API endpoints used by the Collection Rows web enhancement.
/// </summary>
[ApiController]
[Authorize]
[Route("CollectionRows")]
public class CollectionRowsController : ControllerBase
{
    private readonly ILibraryManager _libraryManager;
    private readonly CollectionSectionGroupingService _groupingService;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="CollectionRowsController"/> class.
    /// </summary>
    /// <param name="libraryManager">
    /// Provides access to items in the Jellyfin library.
    /// </param>
    /// <param name="groupingService">
    /// Groups collection items into configured rows.
    /// </param>
    public CollectionRowsController(
        ILibraryManager libraryManager,
        CollectionSectionGroupingService groupingService)
    {
        _libraryManager = libraryManager;
        _groupingService = groupingService;
    }

    /// <summary>
    /// Confirms that the Collection Rows API is available.
    /// </summary>
    /// <returns>A message confirming that the API is running.</returns>
    [AllowAnonymous]
    [HttpGet("Status")]
    public ActionResult<string> GetStatus()
    {
        return Ok("Collection Rows API is running.");
    }

    /// <summary>
    /// Gets the configured rows for a Jellyfin collection.
    /// </summary>
    /// <param name="collectionId">
    /// The Jellyfin collection identifier.
    /// </param>
    /// <returns>
    /// The collection and its grouped rows.
    /// </returns>
    [AllowAnonymous]
    [HttpGet("{collectionId}")]
    public ActionResult<CollectionRowsResponse> GetCollectionRows(
        Guid collectionId)
    {
        BoxSet? collection =
            _libraryManager.GetItemById<BoxSet>(collectionId);

        if (collection is null)
        {
            return NotFound();
        }

        BaseItem[] collectionItems =
            collection.GetLinkedChildren().ToArray();

        Dictionary<Guid, IReadOnlyCollection<string>> items = new();

        foreach (BaseItem item in collectionItems)
        {
            IReadOnlyCollection<string> tags =
                item.Tags?.ToArray() ?? Array.Empty<string>();

            items[item.Id] = tags;
        }

        IEnumerable<CollectionSection> configuredSections;

        if (Plugin.Instance is null)
        {
            configuredSections = Array.Empty<CollectionSection>();
        }
        else
        {
            configuredSections =
                Plugin.Instance.Configuration.Sections;
        }

        var groups =
            _groupingService.CreateGroups(
                items,
                configuredSections);

        CollectionRowsResponse response = new()
        {
            CollectionId = collection.Id,
            CollectionName = collection.Name
        };

        foreach (var group in groups)
        {
            CollectionRowResponse row = new()
            {
                Name = group.Name,
                Order = group.Order
            };

            foreach (Guid id in group.ItemIds)
            {
                row.ItemIds.Add(id);
            }

            response.Rows.Add(row);
        }

        return Ok(response);
    }
}
