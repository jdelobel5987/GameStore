using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Services;

/// <summary>
/// Service for managing game-related operations.
/// </summary>
public class GameService
{
    /// <summary>
    /// Generates the next available ID for a game in the list.
    /// </summary>
    /// <param name="games">The list of existing games.</param>
    /// <returns>The next available ID, or 1 if the list is empty.</returns>
    public static int NextId(List<GameDto> games)
    {
        return games.Count > 0 ? games.Max(game => game.Id) + 1 : 1;
    }
}
