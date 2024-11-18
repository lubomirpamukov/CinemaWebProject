using CinemaWeb.Models;
using CinemaWeb.ViewModels.Movie;
using CinemaWeb.ViewModels.ViewModels.Movie;

namespace CinemaWeb.Services.Interfaces;

public  interface IMovieService
{
    public Task <IEnumerable<MovieIndexViewModel>> GetAllMoviesAsync();

    public Task<bool> AddMovieAsync(MovieCreateViewModel movie);

    public Task<MovieDetailsViewModel> GetMovieDetailsByIdAsync(int id);

    public Task <AddMovieToCinemaProgramViewModel?> GetAddMovieToCinemaInputModelByIdAsync(int id);

    public Task<bool> AddMovieToCinemaAsync(int id, AddMovieToCinemaProgramViewModel model);

    public Task<bool> AddToProgramPostAsync(AddMovieToCinemaProgramViewModel viewModel);

    public Task<EditMovieFormModel?> GetMovieEditModelByIdAsync(int id);

    public Task<bool> UpdateMovieAsync(EditMovieFormModel model);
}
