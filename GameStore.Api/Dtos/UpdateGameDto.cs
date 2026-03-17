using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// DTO de type record class pour représenter les données de mise à jour d'une entrée 'jeu vidéo' dans l'API
/// </summary>
/// <param name="Name">Nom du produit</param>
/// <param name="GenreId">ID du genre du produit</param>
/// <param name="Price">Prix du produit</param>
/// <param name="ReleaseDate">Date de sortie du produit</param>

public record class UpdateGameDto(
    [Required] [StringLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate
);
