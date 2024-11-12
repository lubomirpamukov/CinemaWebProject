using CinemaWebProject.Models;
using CinemaWebProject.ViewModels.Movie;

namespace CinemaWeb.Services.Interfaces;

public  interface IMovieService
{
    public Task <IEnumerable<MovieIndexViewModel>> GetAllMoviesAsync();

    public Task<Movie> CreateAsync(MovieCreateViewModel movie);

    public Task<MovieDetailsViewModel> GetDetailsAsync(int id);

    public Task <AddMovieToCinemaProgramViewModel> AddToProgramGetAsync(int id);

    public Task<bool> AddToProgramPostAsync(AddMovieToCinemaProgramViewModel viewModel);
}
