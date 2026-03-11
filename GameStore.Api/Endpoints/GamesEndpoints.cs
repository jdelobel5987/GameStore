using GameStore.Api.Dtos; // les DTOs
using GameStore.Api.Services; // service pour la logique métier

namespace GameStore.Api.Endpoints;

/// <summary>
/// Endpoints for managing games in the Game Store API.
/// </summary>
public static class GamesEndpoints
{

    const string GetGameEndpointName = "GetGameById";

    /// <summary>
    /// In-memory list of games to simulate a database.
    /// </summary>
    private static readonly List<GameDto> games = [
        new GameDto (1 , "game 1", "fighting", 19.99M, new DateOnly(2024, 6, 1)),
        new GameDto (2 , "game 2", "racing", 29.99M, new DateOnly(2020, 3, 25)),
        new GameDto (3 , "game 3", "sport", 9.99M, new DateOnly(2019, 6, 12)),
        new GameDto (4 , "game 4", "platformer", 39.99M, new DateOnly(2019, 7, 1)),
        new GameDto (5 , "game 5", "rpg", 59.99M, new DateOnly(2023, 11, 8)),
    ];

    /// <summary>
    /// Gets the root endpoint for the API.
    /// </summary>
    /// <returns>A string containing the welcome message.</returns>
    public static string GetRoot() => "Welcome to the Game Store API!\nThis API allows you to manage a collection of video games, including creating, retrieving, updating, and deleting game entries.\nAccess the /swagger endpoint for interactive API documentation and testing.";

    /// <summary>
    /// Retrieves the list of all games in the store.
    /// </summary>
    /// <returns>A list of all available games.</returns>
    
    // public static IResult GetGames() => Results.Ok(games); // I've changed this to return directly the list of games in order to let swagger detect the schema of GameDto
    public static List<GameDto> GetGames() => games;

    /// <summary>
    /// Retrieves a specific game by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the game to retrieve.</param>
    /// <returns>The game if found, a not found result otherwise.</returns>
    public static IResult GetGameById(int id)
        {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }

    /// <summary>
    /// Creates a new game entry.
    /// </summary>
    /// <param name="newGame">The details of the new game to create.</param>
    /// <returns>The created game.</returns>
    public static IResult CreateGame(CreateGameDto newGame)
        {
            GameDto game = new GameDto(
                GameService.NextId(games),
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }    

    /// <summary>
    /// Updates an existing game entry.
    /// </summary>
    /// <param name="id">The unique identifier of the game to update.</param>
    /// <param name="updatedGame">The updated details of the game.</param>
    /// <returns>A no content result if successful, or a not found result if the game is not found.</returns>
    public static IResult UpdateGame(int id, UpdateGameDto updatedGame)
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound(); // or choose to create instead of update if not exists
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        }

    /// <summary>
    /// Deletes a game entry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the game to delete.</param>
    /// <returns>A no content result if successful, or a not found result if the game is not found.</returns>
    public static IResult DeleteGame(int id)
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
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
        group.MapGet("/", GetGames)
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
