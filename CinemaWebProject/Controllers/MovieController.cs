using Microsoft.AspNetCore.Mvc;
using CinemaWebProject.Models;
using CinemaWebProject.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using CinemaWebProject.ViewModels.Cinema;
using CinemaWeb.Data.Models;

namespace CinemaWebProject.Controllers;

public class MovieController(CinemaDbContext context) : Controller
{
    private readonly CinemaDbContext _context = context;

    public async Task<IActionResult> Index()
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
       
        return View(viewModel);
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
                Genre = movie.Genre,
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
    public async Task<IActionResult> AddToProgram(int movieid) 
    {
        //Get movie
        Movie? movie = await _context.Movies.FindAsync(movieid);
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
