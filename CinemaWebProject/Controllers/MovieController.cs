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
        return (await _movieService.GetDetailsAsync(id)) is MovieDetailsViewModel movieDetails
                ? View(movieDetails)
                : NotFound();

    }

    [HttpGet]
    public async Task<IActionResult> AddToProgram(int id) 
    {
        var viewModel = await _movieService.AddToProgramGetAsync(id);

        if (viewModel == null) 
        {
            return RedirectToAction("Index");
        }

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

        var addMovieToProgram = await _movieService.AddToProgramPostAsync(viewModelData);

        if (addMovieToProgram)
        {
            return RedirectToAction("Index");
        }

        return NotFound();
    }
}
