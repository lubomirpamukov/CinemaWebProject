namespace CinemaWeb.ViewModels.Movie;

public class MovieProgramViewModel
{
    //No need for vaidation attributes here,becouse this view model will take data from the database
    //where the data is sanitized
    public string Title { get; set; } = null!;

    public int Duration { get; set; }
}
