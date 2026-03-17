namespace GameStore.Api.Models;

/// <summary>
/// Represents a video game in the Game Store application. This class defines the properties of a game, including its unique identifier (Id), name, genre, price, and release date. The Genre property is a navigation property that allows for the association of a game with a specific genre, while the GenreID property serves as a foreign key to link the game to its genre without needing to load the entire Genre entity. This design facilitates efficient data retrieval and manipulation while maintaining the integrity of the relationship between games and genres in the database.
/// </summary>
public class Game
{
    /// <summary>
    /// Gets or sets the unique identifier for the game. This property serves as the primary key in the database and is typically generated automatically when a new game record is created. It is used to uniquely identify each game in the Game Store application and is essential for database operations such as retrieval, updates, and deletions.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the game. This property is required and must be provided when creating or updating a game record. The name is a crucial piece of information for identifying and displaying the game in the application, and it may also be used for search and filtering purposes.
    /// </summary>
    public required string Name { get; set; }
 
    /// <summary>
    /// Gets or sets the genre of the game. This is a navigation property that allows for the association of a game with a specific genre. The Genre property is typically used to access the details of the genre associated with the game, such as its name. It is important to note that this property may be null if the game does not have an associated genre, which is why it is defined as nullable (Genre?). The GenreID property serves as a foreign key to link the game to its genre without needing to load the entire Genre entity, allowing for more efficient data retrieval and manipulation.
    /// </summary>
    public Genre? Genre { get; set; }   // Navigation property to Genre

    /// <summary>
    /// Gets or sets the foreign key for the genre of the game. This property is used to link the game to its associated genre in the database without needing to load the entire Genre entity. By using GenreID as a foreign key, it allows for more efficient data retrieval and manipulation when working with games and their genres, as it avoids unnecessary joins with the Genre table when only the genre's identifier is needed. This design choice helps to optimize performance while maintaining the integrity of the relationship between games and genres in the database.
    /// </summary>
    public int GenreID { get; set; }    // Foreign key to Genre (avoid to request the whole Genre table and more easily set/update the genre of a game)

    /// <summary>
    /// Gets or sets the price of the game. This property represents the cost of purchasing the game in the Game Store application. It is defined as a decimal to allow for precise representation of currency values, and it is an important piece of information for both customers and administrators when managing the game's listing and sales in the store.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the release date of the game. This property represents the date on which the game was released to the public. It is defined as a DateOnly type, which allows for the representation of a date without a time component, making it suitable for this context. The release date is an important piece of information for customers and administrators alike, as it can influence purchasing decisions and provide context about the game's availability and relevance in the market.
    /// </summary>
    public DateOnly ReleaseDate { get; set; }

}