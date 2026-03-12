using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)   // inherits from DbContext, the main class responsible for interacting with the database in Entity Framework Core
{
    public DbSet<Game> Games => Set<Game>();   // DbSet represents a collection of entities of a specific type that can be queried from the database; here, it represents the collection of Game fields in the database

    public DbSet<Genre> Genres => Set<Genre>();   

}
