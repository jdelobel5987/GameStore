using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// The GameStoreContext class is a DbContext that represents the session with the database and allows to query and save instances of our entities. It is configured to use the options provided in the constructor, which typically include the connection string and other database-related settings. 
/// </summary>
/// <param name="options">
/// The options for configuring the database context. This typically includes the connection string and other database-related settings. It is passed to the base DbContext class to initialize the context with the specified options.
/// </param> 
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)   // inherits from DbContext, the main class responsible for interacting with the database in Entity Framework Core
{
    /// <summary>
    /// The Games property is a DbSet of Game entities, which allows us to perform CRUD operations on the Game table in the database. It represents the collection of Game entities that can be queried and manipulated.
    /// </summary>
    public DbSet<Game> Games => Set<Game>();   // DbSet represents a collection of entities of a specific type that can be queried from the database; here, it represents the collection of Game fields in the database

    /// <summary>
    /// The Genres property is a DbSet of Genre entities, which allows us to perform CRUD operations on the Genre table in the database. It represents the collection of Genre entities that can be queried and manipulated.
    /// </summary>
    public DbSet<Genre> Genres => Set<Genre>();   

}
