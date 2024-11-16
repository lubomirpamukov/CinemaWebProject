using CinemaWeb.Data;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.Models;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using CinemaWeb.Infrastructure.Repository.Interfaces;

namespace CinemaWeb.Services.Services;

public class MovieService
    (
        IRepository<Movie> movieRepository,
        IRepository<Cinema> cinemaRepository,
        IRepository<CinemaMovie> cinemaMovieRepository

    ):IMovieService
{
    private readonly IRepository<Movie> _movies = movieRepository;
    private readonly IRepository<Cinema> _cinema = cinemaRepository;
    private readonly IRepository<CinemaMovie> _cinemaMovie = cinemaMovieRepository;
    public async Task<AddMovieToCinemaProgramViewModel> AddToProgramGetAsync(int id)
    {
        //Get movie
        Movie? movie = await _movies.FindByIdAsync(id);
        
        //get all cinemas
        var cinemas = await _cinema.GetAllAsync();

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
        var movieCinemaRelations = await _cinemaMovie
                                         .GetAllAttachedAsync()
                                         .Where(cm => cm.MovieId == viewModelData.MovieId)
                                         .ToArrayAsync();

        await _cinemaMovie.DeleteRangeAsync(movieCinemaRelations);

        var cinemaMovieToAdd = new List<CinemaMovie>();
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
                cinemaMovieToAdd.Add(cinemaMovie);
            }
        }
        // saving changes to the database
        await _cinemaMovie.AddRangeAsync(cinemaMovieToAdd);

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
            ReleaseDate = movie.ReleaseDate,
            ImageUrl = movie.ImageUrl
        };

        if (movieToAdd == null) 
        {
            return false;
        }

        await _movies.AddAsync(movieToAdd);
        return true;
    }

    public async Task<IEnumerable<MovieIndexViewModel>> GetAllMoviesAsync()
    {
        var viewModel = new List<MovieIndexViewModel>();
        var movies = await _movies.GetAllAsync();

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
        Movie? movie = await _movies.FindByIdAsync(id);

        MovieDetailsViewModel viewModel = new MovieDetailsViewModel
        {
            Title = movie.Title,
            Genre = movie.Genre!,
            ReleaseDate = movie.ReleaseDate,
            Director = movie.Director,
            Duration = movie.Duration,
            Description = movie.Description,
            ImageUrl = movie.ImageUrl
        };

        return viewModel;
    }
}
