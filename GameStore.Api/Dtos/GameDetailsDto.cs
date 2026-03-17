namespace GameStore.Api.Dtos;

/// <summary>
/// DTO representing the details of a game in the GameStore API.
/// </summary>
/// <param name="Id">The unique identifier of the game.</param>
/// <param name="Name">The name of the game.</param>
/// <param name="GenreId">The ID of the genre to which the game belongs.</param>
/// <param name="Price">The price of the game.</param>
/// <param name="RealeaseDate">The release date of the game.</param>
public record class GameDetailsDto
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly RealeaseDate

);