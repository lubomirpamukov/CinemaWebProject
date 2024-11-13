using CinemaWeb.Data.Models;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Controllers;

public class CinemaController(ICinemaService cinemaService) : Controller
{
    private readonly ICinemaService _cinemaService = cinemaService;

    public async Task<IActionResult> Index()
    {
        return View(await _cinemaService.GetAllAsync());
    }

    public IActionResult Create() 
    {
        return View(new CinemaCreateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CinemaCreateViewModel viewModel) 
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);

        }

        var isCinemaCreated = await _cinemaService.CreateAsync(viewModel);

        if (isCinemaCreated) 
        {
           return RedirectToAction("Index");
        }

        return NotFound();
    }

    public async Task<IActionResult> ViewMovieProgram(int id) 
    {
        var cinemaViewMoviesViewModel = await _cinemaService.ViewMovieProgramAsync(id);

        if (cinemaViewMoviesViewModel == null) 
        {
            return NotFound();
        }

        return View(cinemaViewMoviesViewModel);
    } 
}
