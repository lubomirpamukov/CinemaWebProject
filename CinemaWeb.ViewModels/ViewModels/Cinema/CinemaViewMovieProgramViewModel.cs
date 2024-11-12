using CinemaWeb.ViewModels.Movie;

namespace CinemaWeb.ViewModels.Cinema;

public class CinemaViewMovieProgramViewModel
{
    //This view model don't need constraints data annotations becouse it dosn't work with user input

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public ICollection<MovieProgramViewModel> Movies { get; set; }
        = new HashSet<MovieProgramViewModel>(); 
}
