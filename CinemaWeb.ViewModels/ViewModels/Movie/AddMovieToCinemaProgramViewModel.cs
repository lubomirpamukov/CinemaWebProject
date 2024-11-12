using CinemaWeb.ViewModels.Cinema;

namespace CinemaWeb.ViewModels.Movie
{
    public class AddMovieToCinemaProgramViewModel
    {
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public List<CinemaCheckBoxItemViewModel> Cinemas { get; set; }
            = new List<CinemaCheckBoxItemViewModel>();
    }
}
