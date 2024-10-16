using System.ComponentModel.DataAnnotations;
using static CinemaWebProject.Common.Constants.Movie;
namespace CinemaWebProject.ViewModels.Movie;

public class CreateViewModel
{
    [Required]
    [MaxLength(TitleMaxLength)]
    [MinLength(TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(GenreMaxLength)]
    [MinLength(GenreMinLength)]
    public string Genre { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [MaxLength(DirectorMaxLength)]
    [MinLength(DirectorMinLength)]
    public string? Director { get; set; }

    [Required]
    [Range(DurationMinLength, DurationMaxLength)]
    public int Duration { get; set; }

    [MaxLength(DurationMaxLength)]
    [MinLength(DurationMinLength)]
    public string? Description { get; set; }
}
