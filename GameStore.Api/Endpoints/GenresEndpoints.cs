using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

/// <summary>
/// Endpoints for managing genres in the Game Store API.
/// </summary>
public static class GenresEndpoints
{
    /// <summary>
    /// Maps the endpoints related to genres to the provided WebApplication instance.
    /// </summary>
    /// <param name="app">The WebApplication instance to map the endpoints to.</param>
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres")
                        .WithTags("Genres")
            ;

        // GET /genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                .Select(genre => new GenreDto(genre.Id, genre.Name))
                .AsNoTracking()
                .ToListAsync()
        );
    }
}
