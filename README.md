# Collection Rows

A Jellyfin plugin by **QDRD Media** that organizes items inside a single collection into configurable sections.

Instead of displaying every item in one continuous list, Collection Rows groups items into configurable rows such as:

- Movies
- Documentaries
- Fan Films
- Specials
- Custom sections

Items remain members of the same Jellyfin collection. No duplicate collections are created, and existing library organization is preserved.

## Features (Planned)

- Organize collection items into multiple rows using metadata tags.
- Configurable section names and display order.
- Default "Movies" section for items without a matching tag.
- Compatible with existing Jellyfin collections.
- Designed to work alongside other collection-related plugins.

## Example

Tags:

```
CollectionSection:Documentary
CollectionSection:Fan Film
CollectionSection:Special
```

Results:

```
Friday the 13th Collection

Movies
• Friday the 13th
• Friday the 13th Part 2
• Friday the 13th Part III

Documentaries
• Crystal Lake Memories
• Never Sleep Again

Fan Films
• Dylan's New Nightmare

Specials
• Reunion Featurette
```

## Current Status

🚧 **Early Development**

The core data models and grouping logic are complete. Jellyfin integration is currently in active development.

## Roadmap

- [x] Core section models
- [x] Tag matching service
- [x] Grouping service
- [ ] Jellyfin service registration
- [ ] Configuration UI
- [ ] Collection page integration
- [ ] Public release

## Compatibility

- Jellyfin 10.11.11+
- .NET 9

## License

This project is licensed under the GNU General Public License v3.0.

## About QDRD Media

QDRD Media develops open-source software focused on media organization, companion viewing experiences, and tools for personal media servers.

---

*Collection Rows is not affiliated with or endorsed by the Jellyfin Project.*
