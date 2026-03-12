using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();   // scope needed to get access to an instance of GameStoreContext
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>(); // retrieve an instance of GameStoreContext from the service provider
        dbContext.Database.Migrate(); // applies any pending migrations to the DB
    }

}
