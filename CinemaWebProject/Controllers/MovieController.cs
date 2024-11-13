using Microsoft.AspNetCore.Mvc;
using CinemaWeb.Models;
using CinemaWeb.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.Data.Models;
using CinemaWeb.Services.Interfaces;

namespace CinemaWeb.Controllers;

public class MovieController(IMovieService movieService) : Controller
{
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

        if (!ModelState.IsValid) 
        {
            return View(movie);
        }

        var movieToAdd = await _movieService.CreateAsync(movie);

        if (movieToAdd) 
        {
            return RedirectToAction("Index");
        }

        return NotFound();
    }

    public async Task<IActionResult> Details(int id)
    {
        return (await _movieService.GetDetailsAsync(id)) is MovieDetailsViewModel movieDetails
                ? View(movieDetails)
                : NotFound();

    }

    [HttpGet]
    public async Task<IActionResult> AddToProgram(int movieId) 
    {
        var viewModel = await _movieService.AddToProgramGetAsync(movieId);

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
