namespace GameStore.Api.Models;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }
 
    public Genre? Genre { get; set; }   // Navigation property to Genre

    public int GenreID { get; set; }    // Foreign key to Genre (avoid to request the whole Genre table and more easily set/update the genre of a game)

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

}