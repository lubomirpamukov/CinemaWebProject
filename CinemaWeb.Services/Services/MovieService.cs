using CinemaWeb.Data.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWebProject.Models;
using CinemaWebProject.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class MovieService(CinemaDbContext dbContext) : IMovieService
{
    private readonly CinemaDbContext _context = dbContext;
    public Task<bool> AddToProgramAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Movie> CreateAsync(MovieCreateViewModel movie)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MovieIndexViewModel>> GetAllMoviesAsync()
    {
        var viewModel = new List<MovieIndexViewModel>();
        var movies = await _context.Movies.ToListAsync();

        foreach (var movie in movies)
        {
            var movieView = new MovieIndexViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Duration = movie.Duration,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
            };

            viewModel.Add(movieView);
        }

        return viewModel;
    }

    public Task<MovieDetailsViewModel> GetDetailsAsync(int id)
    {
        throw new NotImplementedException();
    }
}
