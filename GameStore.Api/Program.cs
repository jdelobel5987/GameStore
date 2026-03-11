using GameStore.Api.Endpoints;
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

var app = builder.Build();

// Activer Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGamesEndpoints(); // map les endpoints de jeux depuis la classe GamesEndpoints

app.Run(); 