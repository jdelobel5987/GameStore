using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// DTO de type record class pour représenter les données de création d'une entrée 'jeu vidéo' dans l'API
/// </summary>
/// <param name="Name">Nom du produit</param>
/// <param name="Genre">Genre du produit</param>
/// <param name="Price">Prix du produit</param>
/// <param name="ReleaseDate">Date de sortie du produit</param>
public record class CreateGameDto
(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(20)] string Genre,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate
);
