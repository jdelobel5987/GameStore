using GameStore.Api.Dtos; // les DTOs
using GameStore.Api.Services; // le service pour la logique métier
using System.Reflection; // pour inclure les commentaires XML dans Swagger


var builder = WebApplication.CreateBuilder(args);


///////////////
// Services ///
///////////////

// Ajouter Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>   // ok sans options; ces options permettent d'inclure les commentaires XML dans Swagger
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);
}
);

var app = builder.Build();

// Activer Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


///////////////////////////////////////
// List de jeux pour simuler une BDD //
///////////////////////////////////////

List<GameDto> games = [
    new GameDto (1 , "game 1", "fighting", 19.99M, new DateOnly(2024, 6, 1)),
    new GameDto (2 , "game 2", "racing", 29.99M, new DateOnly(2020, 3, 25)),
    new GameDto (3 , "game 3", "sport", 9.99M, new DateOnly(2019, 6, 12)),
    new GameDto (4 , "game 4", "platformer", 39.99M, new DateOnly(2019, 7, 1)),
    new GameDto (5 , "game 5", "rpg", 59.99M, new DateOnly(2023, 11, 8)),
];


///////////////
// Endpoints //
///////////////

app.MapGet("/", () => "Hello World!")
   .WithSummary("Root endpoint")
   .WithDescription("This endpoint returns a simple greeting message to confirm that the API is up and running.")
//    .WithTags("Root")
;

// GET /games
app.MapGet("/games", () => games)
   .WithSummary("Get all games")
   .WithDescription("This endpoint returns a list of all available games.")
//    .WithTags("Games")
;


// GET /games/{id}
const string GetGameEndpointName = "GetGameById";

app.MapGet("/games/{id}", (int id) => 
{
    var game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
   .WithName(GetGameEndpointName) // name endpoint to reuse it in other endpoints
   .WithSummary("Get game by ID")
   .WithDescription("This endpoint returns a specific game based on its ID.")
    // .WithTags("Games/id")
;

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
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
})
    .WithSummary("Create a game entry")
    .WithDescription("This endpoint creates a new game entry.")
    // .WithTags("Games")

;

// PUT /games/{id}
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
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
})
   .WithSummary("Update a game entry by ID")
   .WithDescription("This endpoint updates an existing game entry. If the game with the specified ID does not exist, it returns a 404 Not Found response.")
    // .WithTags("Games/id")
;

// DELETE /games/{id}
app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent(); 
})
    .WithSummary("Delete a game entry by ID")
    .WithDescription("This endpoint deletes an existing game entry based on its ID.")
    // .WithTags("Games/id")

;


// app.UseHttpsRedirection();

app.Run(); 