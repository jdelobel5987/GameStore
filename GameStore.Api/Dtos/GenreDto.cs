namespace GameStore.Api.Dtos;

/// <summary>
/// Data Transfer Object (DTO) for representing a genre in the Game Store API. 
/// </summary>
/// <param name="Id">The unique identifier for the genre.</param>
/// <param name="Name">The name of the genre.</param>
public record class GenreDto (int Id, string Name);

