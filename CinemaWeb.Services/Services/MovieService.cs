using CinemaWeb.Data.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.Models;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class MovieService(CinemaDbContext dbContext) : IMovieService
{
    private readonly CinemaDbContext _context = dbContext;
    public async Task<AddMovieToCinemaProgramViewModel> AddToProgramGetAsync(int id)
    {
        //Get movie
        Movie? movie = await _context.Movies.FindAsync(id);
        
        //get all cinemas
        var cinemas = await _context.Cinemas.ToListAsync();

        AddMovieToCinemaProgramViewModel viewModel = new AddMovieToCinemaProgramViewModel
        {
            MovieId = movie.Id,
            MovieTitle = movie.Title,
            Cinemas = cinemas.Select(cinema => new CinemaCheckBoxItemViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                IsSelected = false
            }).ToList()
        };

        return viewModel;
    }

    public async Task<bool> AddToProgramPostAsync(AddMovieToCinemaProgramViewModel viewModelData)
    {
        if (viewModelData == null) 
        {
            return false;
        }
        //removing previous connection with movie cinemas
        var movieCinemaRelations = await _context.CinemasMovies
                                         .Where(cm => cm.MovieId == viewModelData.MovieId)
                                         .ToArrayAsync();

        _context.RemoveRange(movieCinemaRelations);
        await _context.SaveChangesAsync();

        //Adding the movie to the selected cinemas
        foreach (var cinema in viewModelData.Cinemas)
        {
            if (cinema.IsSelected)
            {
                //Creating a new mapping table entity
                var cinemaMovie = new CinemaMovie
                {
                    CinemaId = cinema.Id,
                    MovieId = viewModelData.MovieId
                };

                //adding cinemaMovies mapping entity to the database
                _context.CinemasMovies.Add(cinemaMovie);
            }
        }
        // saving changes to the database
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CreateAsync(MovieCreateViewModel movie)
    {

        Movie movieToAdd = new Movie
        {
            Title = movie.Title,
            Description = movie.Description,
            Director = movie.Director,
            Duration = movie.Duration,
            Genre = movie.Genre,
            ReleaseDate = movie.ReleaseDate
        };

        if (movieToAdd == null) 
        {
            return false;
        }

        await _context.Movies.AddAsync(movieToAdd);
        await _context.SaveChangesAsync();

        return true;
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

    public async Task<MovieDetailsViewModel> GetDetailsAsync(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);

        MovieDetailsViewModel viewModel = new MovieDetailsViewModel
        {
            Title = movie.Title,
            Genre = movie.Genre!,
            ReleaseDate = movie.ReleaseDate,
            Director = movie.Director,
            Duration = movie.Duration,
            Description = movie.Description
        };

        return viewModel;
    }
}
