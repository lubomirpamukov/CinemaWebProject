using System.ComponentModel.DataAnnotations;
using static CinemaWebProject.Common.Constants.Movie;
namespace CinemaWebProject.ViewModels.Movie;

public class MovieCreateViewModel
{
    public MovieCreateViewModel()
    {
        ReleaseDate = DateTime.Now;
    }
    [Required(ErrorMessage = TitleError)]
    [MaxLength(TitleMaxLength, ErrorMessage = TitleLengthError)]
    [MinLength(TitleMinLength, ErrorMessage = TitleLengthError)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = GenreError)]
    [MaxLength(GenreMaxLength)]
    [MinLength(GenreMinLength)]
    public string Genre { get; set; }

    [Required(ErrorMessage = ReleaseDateError)]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = DirectorError)]
    [MaxLength(DirectorMaxLength, ErrorMessage = DirectorNameLengthError)]
    [MinLength(DirectorMinLength,ErrorMessage = DirectorNameLengthError)]
    public string Director { get; set; }

    [Required(ErrorMessage = DurationError)]
    [Range(DurationMinLength, DurationMaxLength,ErrorMessage = DurationRangeError)]
    public int Duration { get; set; }

    [MaxLength(DurationMaxLength)]
    [MinLength(DurationMinLength)]
    public string? Description { get; set; }
}
