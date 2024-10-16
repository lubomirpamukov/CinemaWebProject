using System.ComponentModel.DataAnnotations;

namespace CinemaWebProject.ViewModels.Movie;

public class IndexViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public string? Genre { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    public int Duration { get; set; }
}
