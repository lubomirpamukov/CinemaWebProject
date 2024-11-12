using System.ComponentModel.DataAnnotations;
using static CinemaWeb.Common.Constants.Movie;

namespace CinemaWeb.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(GenreMaxLength)]
    public string? Genre { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [MaxLength(DirectorMaxLength)]
    public string Director { get; set; }

    [Required]
    [Range(DurationMinLength,DurationMaxLength)]
    public int Duration { get; set; }

    [MaxLength(DurationMaxLength)]
    public string? Description { get; set; }

    public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
        = new HashSet<CinemaMovie>();

}
