using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.AspNetCore.Http.Connections;
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

// utiliser les annotations de validation des DTOs
builder.Services.AddValidation();

// Ajouter le contexte de données
builder.AddGameStoreDb(); 

// Ajouter CORS pour autoriser les requêtes depuis le frontend React
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp",policy =>
    {
        policy.WithOrigins("http://localhost:5173") // URL du frontend React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Activer Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGamesEndpoints(); // map les endpoints de jeux depuis la classe GamesEndpoints

app.MigrateDb(); // applies any pending migrations to the DB (from Data\DataExtensions.cs)

app.UseCors("ReactApp"); // appliquer la politique CORS pour autoriser les requêtes depuis le frontend React

app.Run(); 