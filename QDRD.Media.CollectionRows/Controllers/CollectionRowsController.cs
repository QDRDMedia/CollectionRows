using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    public CollectionRowsController(
        ILibraryManager libraryManager,
        CollectionSectionGroupingService groupingService)
    {
        _libraryManager = libraryManager;
        _groupingService = groupingService;
    }

    [AllowAnonymous]
    [HttpGet("Status")]
    public ActionResult<string> GetStatus()
    {
        return Ok("Collection Rows API is running.");
    }

    [AllowAnonymous]
    [HttpGet("{collectionId}")]
    public ActionResult<CollectionRowsResponse> GetCollectionRows(Guid collectionId)
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

        // Temporary configuration until the settings page exists.
        List<CollectionSection> configuredSections =
        [
            new CollectionSection
            {
                Name = "Documentaries",
                Order = 10,
                Tag = "CollectionSection:Documentary"
            },
            new CollectionSection
            {
                Name = "Fan Films",
                Order = 20,
                Tag = "CollectionSection:Fan Film"
            }
        ];

        var groups =
            _groupingService.CreateGroups(items, configuredSections);

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
