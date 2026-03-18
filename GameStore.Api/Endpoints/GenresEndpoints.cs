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
    /// Retrieves all genres from the database and returns them as a list of GenreDto objects.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <returns>A list of GenreDto objects.</returns>
    public static async Task<List<GenreDto>> GetAllGenres(GameStoreContext dbContext)
    {
        return await dbContext.Genres
            .Select(genre => new GenreDto(genre.Id, genre.Name))
            .AsNoTracking()
            .ToListAsync();
    }
    
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
        group.MapGet("/", GetAllGenres)
            .WithSummary("Retrieves all genres")
            .WithDescription("This endpoint returns a list of all genres available in the Game Store database.")
            ;
    }
}
