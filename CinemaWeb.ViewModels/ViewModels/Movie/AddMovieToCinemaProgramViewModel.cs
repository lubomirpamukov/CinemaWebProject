using CinemaWebProject.ViewModels.Cinema;

namespace CinemaWebProject.ViewModels.Movie
{
    public class AddMovieToCinemaProgramViewModel
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public List<CinemaCheckBoxItemViewModel> Cinemas { get; set; }
            = new List<CinemaCheckBoxItemViewModel>();
    }
}
