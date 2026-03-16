using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// Extension methods for database operations.
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Applies any pending migrations to the database. This method is typically called during application startup to ensure that the database schema is up to date with the application's data model. It creates a scope to retrieve an instance of GameStoreContext and calls the Migrate method on the database, which applies any pending migrations. If the database does not exist, it will be created automatically. If there are no pending migrations, this method will do nothing. This is a crucial step in ensuring that the application can interact with the database correctly, especially after changes to the data model that require schema updates.
    /// </summary>
    /// <param name="app">
    /// The WebApplication instance for which to apply migrations.
    /// </param>
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();   // scope needed to get access to an instance of GameStoreContext
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>(); // retrieve an instance of GameStoreContext from the service provider
        dbContext.Database.Migrate(); // applies any pending migrations to the DB
    }

    /// <summary>
    /// Registers the GameStoreContext with the dependency injection container using a connection string for a SQLite database. This method defines the connection string for the SQLite database and then registers the GameStoreContext with the specified connection string. Additionally, it includes a seeding mechanism that checks if there are any existing genres in the database, and if not, it adds a predefined list of genres (Fighting, RPG, Platformer, Racing, Sports) to the database. This ensures that when the application is first run, it has some initial data to work with. The method is designed to be called during application startup to set up the database context for the application.
    /// </summary>
    /// <param name="builder">
    /// The WebApplicationBuilder instance for which to add the database context.
    /// </param>
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        // Define connection string for SQLite database
        var connString = "Data Source=GameStore.db"; // Beware: the string syntax depends on the DB provider (here SQLite)

        // Register GameStoreContext with the connection string
        builder.Services.AddSqlite<GameStoreContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Platformer" },
                        new Genre { Name = "Racing" },
                        new Genre { Name = "Sports" }
                    );

                    context.SaveChanges();
                }
            })
        );
    }

}
