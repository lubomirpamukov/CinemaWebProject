using System.ComponentModel.DataAnnotations;
using static CinemaWebProject.Common.Constants.Movie;
namespace CinemaWebProject.ViewModels.Movie
{
    public class DetailsViewModel
    {

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public int Duration { get; set; }

        public string? Director { get; set; }

        public string? Description { get; set; }

    }
}
