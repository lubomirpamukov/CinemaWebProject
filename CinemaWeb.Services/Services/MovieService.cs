using CinemaWeb.Data;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.Models;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using CinemaWeb.Infrastructure.Repository.Interfaces;
using CinemaWeb.ViewModels.ViewModels.Movie;

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
    public async Task<AddMovieToCinemaProgramViewModel> GetAddMovieToCinemaInputModelByIdAsync(int id)
    {
        //Get movie
        Movie? movie = await _movies.FindByIdAsync(id);
        
        //get all cinemas
        var cinemas = await _cinema.GetAllAsync();
        AddMovieToCinemaProgramViewModel viewModel = new AddMovieToCinemaProgramViewModel
        {
            MovieId = movie.Id,
            MovieTitle = movie.Title,
            Cinemas = cinemas
            .Where(c => c.IsDeleted == false)
            .Select(cinema => new CinemaCheckBoxItemViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                IsSelected = false,
                IsDeleted = cinema.IsDeleted
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
        foreach (var cinema in viewModelData.Cinemas.Where(c => c.IsDeleted == false))
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

    public async Task<bool> AddMovieAsync(MovieCreateViewModel movie)
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

    public async Task<MovieDetailsViewModel> GetMovieDetailsByIdAsync(int id)
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

    public Task<bool> AddMovieToCinemaAsync(int id, AddMovieToCinemaProgramViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<EditMovieFormModel?> GetMovieEditModelByIdAsync(int id)
    {
        var movie = await _movies
            .GetAllAttachedAsync()
            .Where(movie => movie.Id == id)
            .Select(m => new EditMovieFormModel
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate,
                Duration = m.Duration,
                Director = m.Director,
                Description = m.Description,
                ImageUrl = m.ImageUrl
            }).FirstOrDefaultAsync();

        return movie;
    }

    public async Task<bool> UpdateMovieAsync(EditMovieFormModel model)
    {
        var movie = await _movies
            .GetAllAttachedAsync()
            .Where(m => m.Id == model.Id)
            .FirstOrDefaultAsync();

        if (movie == null)
        {
            return false; // no movie found
        }

        movie.Title = model.Title;
        movie.Genre = model.Genre;
        movie.ReleaseDate = model.ReleaseDate;
        movie.Duration = model.Duration;
        movie.Director = model.Director;
        movie.Description = model.Description;
        movie.ImageUrl = model.ImageUrl;

        return await _movies.UpdateAsync(movie);
    }
}
