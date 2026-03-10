namespace GameStore.Api.Dtos;

/// <summary>
/// DTO de type record class pour représenter les données d'un jeu vidéo dans l'API
/// </summary>
/// <param name="Id">Identifiant unique du produit</param>
/// <param name="Name">Nom du produit</param>
/// <param name="Genre">Genre du produit</param>
/// <param name="Price">Prix du produit</param>
/// <param name="ReleaseDate">Date de sortie du produit</param>
public record class GameDto
(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

// DTO : Data Transfer Object
// transfert de données entre les différentes couches de l'application

// DTO de type record / record class
// les propriétés dans les () sont immuables et publiques par défaut

// DTO de type class pour plus de flexibilité dans la définition des propriétés
