using Microsoft.AspNetCore.Mvc;
using CinemaWebProject.Models;
using CinemaWebProject.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using CinemaWebProject.ViewModels.Cinema;
using CinemaWeb.Data.Models;
using CinemaWeb.Services.Interfaces;

namespace CinemaWebProject.Controllers;

public class MovieController(CinemaDbContext context, IMovieService movieService) : Controller
{
    private readonly CinemaDbContext _context = context;
    private readonly IMovieService _movieService = movieService;

    public async Task<IActionResult> Index()
    {
        var getMovies = await _movieService.GetAllMoviesAsync();
        return View(getMovies);
    }

    [HttpGet]
    public IActionResult Create() 
    {
        return View(new MovieCreateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(MovieCreateViewModel movie)
    {

        if (ModelState.IsValid) 
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

            await _context.Movies.AddAsync(movieToAdd);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(movie);
    }

    public async Task<IActionResult> Details(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);

        if (movie != null) 
        {
            MovieDetailsViewModel viewModel = new MovieDetailsViewModel 
            {
                Title = movie.Title,
                Genre = movie.Genre!,
                ReleaseDate = movie.ReleaseDate,
                Director  = movie.Director,
                Duration = movie.Duration,
                Description = movie.Description
            };
            return View(viewModel);
        }

            return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> AddToProgram(int id) 
    {
        //Get movie
        Movie? movie = await _context.Movies.FindAsync(id);
        // check if movie exist
        if (movie == null) 
        {
            return RedirectToAction("Index");
        }
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

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddToProgram(AddMovieToCinemaProgramViewModel viewModelData)
    {
        //validating the data input from the user
        if (!ModelState.IsValid) 
        {
            return View(viewModelData);
        }

        //removing previouse connection with movie cinemas
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

                //adding cinemaMovies mapping entitiy to the database
                _context.CinemasMovies.Add(cinemaMovie);
            }
        }
        // saving changes to the database
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
