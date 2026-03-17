using GameStore.Api.Data;
using GameStore.Api.Dtos; // les DTOs
using GameStore.Api.Models;
using GameStore.Api.Services;
using Microsoft.EntityFrameworkCore; // service pour la logique métier

namespace GameStore.Api.Endpoints;

/// <summary>
/// Endpoints for managing games in the Game Store API.
/// </summary>
public static class GamesEndpoints
{

    const string GetGameEndpointName = "GetGameById";

    /// <summary>
    /// Gets the root endpoint for the API.
    /// </summary>
    /// <returns>A string containing the welcome message.</returns>
    public static string GetRoot() => "Welcome to the Game Store API!\nThis API allows you to manage a collection of video games, including creating, retrieving, updating, and deleting game entries.\nAccess the /swagger endpoint for interactive API documentation and testing.";

    /// <summary>
    /// Retrieves all games from the database and returns them as a list of GameSummaryDto objects.
    /// </summary>
    /// <param name="dbContext">The database context for accessing the data store.</param>
    /// <returns>A list of all available games.</returns>
    public static async Task<List<GameSummaryDto>> GetAllGames(GameStoreContext dbContext)
    {
        return await dbContext.Games
            .Include(game => game.Genre)
            .Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            ))
            .AsNoTracking()     // to optimize read-only queries by disabling change tracking
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific game by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the game to retrieve.</param>
    /// <param name="dbContext">The database context for accessing the data store.</param>
    /// <returns>The game if found, a not found result otherwise.</returns>
    public static async Task<IResult> GetGameById(int id, GameStoreContext dbContext)
        {
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreID,
                game.Price,
                game.ReleaseDate
            ));
        }

    /// <summary>
    /// Creates a new game entry.
    /// </summary>
    /// <param name="newGame">The details of the new game to create.</param>
    /// <param name="dbContext">The database context for accessing the data store.</param>
    /// <returns>The created game.</returns>
    public static async Task<IResult> CreateGame(CreateGameDto newGame, GameStoreContext dbContext)
        {
            Game game = new Game()
            {
                Name = newGame.Name,
                GenreID = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreID,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        }

    /// <summary>
    /// Updates an existing game entry.
    /// </summary>
    /// <param name="id">The unique identifier of the game to update.</param>
    /// <param name="updatedGame">The updated details of the game.</param>
    /// <param name="dbContext">The database context for accessing the data store.</param>
    /// <returns>A no content result if successful, or a not found result if the game is not found.</returns>
    public static async Task<IResult> UpdateGame(int id, UpdateGameDto updatedGame, GameStoreContext dbContext)
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound(); // or choose to create instead of update if not exists
            }

            existingGame.Name = updatedGame.Name;
            existingGame.GenreID = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        }

    /// <summary>
    /// Deletes a game entry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the game to delete.</param>
    /// <param name="dbContext">The database context for accessing the data store.</param>
    /// <returns>A no content result if successful, or a not found result if the game is not found.</returns>
    public static async Task<IResult> DeleteGame(int id, GameStoreContext dbContext)
        {
            // 3 steps deletion: find, remove, save changes
            var game = await dbContext.Games.FindAsync(id);
            if (game is null)
            {
                return Results.NotFound();
            }

            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();

            // 1 step alternative: directly execute a DELETE statement in the DB without loading the entity into memory
            // var deleted = await dbContext.Games
            //     .Where(game => game.Id == id)
            //     .ExecuteDeleteAsync();
            // 
            // return deleted == 0 ? Results.NotFound() : Results.NoContent();

        }

    /// <summary>
    /// Maps the game-related endpoints to the provided WebApplication instance.
    /// </summary>
    /// <param name="app">The WebApplication instance to which the endpoints will be mapped.</param>
    public static void MapGamesEndpoints(this WebApplication app)
    {
        // Route group
        var group = app.MapGroup("/games")
            .WithTags("Games") // tag pour regrouper les endpoints dans Swagger
            // .WithOpenApi() // pour inclure ce groupe dans Swagger
            ;

        // Root endpoint
        app.MapGet("/", GetRoot)
           .WithSummary("Root endpoint")
           .WithDescription("This endpoint returns a simple greeting message to confirm that the API is up and running.")
        //    .WithTags("Root")
        ;

        // GET /games
        group.MapGet("/", GetAllGames)
           .WithSummary("Get all games")
           .WithDescription("This endpoint returns a list of all available games.")
        //    .WithTags("Games")
        ;

        // GET /games/{id}
        group.MapGet("/{id}", GetGameById)
           .WithName(GetGameEndpointName) // name endpoint to reuse it in other endpoints
           .WithSummary("Get game by ID")
           .WithDescription("This endpoint returns a specific game based on its ID.")
        // .WithTags("Games/id")
        ;

        // POST /games
        group.MapPost("/", CreateGame)
            .WithSummary("Create a game entry")
            .WithDescription("This endpoint creates a new game entry.")
        // .WithTags("Games")
        ;

        // PUT /games/{id}
        group.MapPut("/{id}", UpdateGame)
           .WithSummary("Update a game entry by ID")
           .WithDescription("This endpoint updates an existing game entry. If the game with the specified ID does not exist, it returns a 404 Not Found response.")
        // .WithTags("Games/id")
        ;

        // DELETE /games/{id}
        group.MapDelete("/{id}", DeleteGame)
            .WithSummary("Delete a game entry by ID")
            .WithDescription("This endpoint deletes an existing game entry based on its ID.")
        // .WithTags("Games/id")
        ;
    }
}
