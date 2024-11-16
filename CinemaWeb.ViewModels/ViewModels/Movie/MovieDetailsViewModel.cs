using System.ComponentModel.DataAnnotations;
using static CinemaWeb.Common.Constants.Movie;
namespace CinemaWeb.ViewModels.Movie
{
    public class MovieDetailsViewModel
    {

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public int Duration { get; set; }

        public string? Director { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

    }
}
