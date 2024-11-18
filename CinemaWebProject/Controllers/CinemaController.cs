using CinemaWeb.Data.Models;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using CinemaWeb.ViewModels.ViewModels.Cinema;
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

        var isCinemaCreated = await _cinemaService.AddCinemaAsync(viewModel);

        if (isCinemaCreated) 
        {
           return RedirectToAction("Index");
        }

        return NotFound();
    }

    public async Task<IActionResult> ViewMovieProgram(int id) 
    {
        var cinemaViewMoviesViewModel = await _cinemaService.GetCinemaDetailsByIdAsync(id);

        if (cinemaViewMoviesViewModel == null) 
        {
            return NotFound();
        }

        return View(cinemaViewMoviesViewModel);
    }

    public async Task<IActionResult> Manage() 
    {
        IEnumerable<CinemaIndexViewModel> cinemas = await _cinemaService.IndexGetAllOrderedByLocationAsync();
        return View(cinemas);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id) 
    {
        if (id < 1) 
        {
            RedirectToAction(nameof(Manage));
        }

        var cinemaToUpdate = await _cinemaService.GetCinemaEditModelByIdAsync(id);

        if (cinemaToUpdate == null) 
        {
            RedirectToAction(nameof(Manage));
		}

        return View(cinemaToUpdate);
    }

    [HttpPost]

    public async Task<IActionResult> Edit(int id,EditCinemaFormModel model) 
    {
        if (!ModelState.IsValid) 
        {
            return View(model);
        }

        if (id != model.Id) 
        {
            return RedirectToAction(nameof(Manage));
        }

        bool isUpdated = await _cinemaService.UpdateCinemaAsync(model);

        if (!isUpdated) 
        {
            ModelState.AddModelError(string.Empty, "Unable to update cinema");
            return View(model);
        }

        return RedirectToAction(nameof(Manage));
    }

    [HttpGet]
    public async Task<IActionResult> SoftDelete(int id) 
    {
        if (id < 1)
        {
            return RedirectToAction(nameof(Manage));
        }

        var cinema = await _cinemaService
            .GetViewDetailsAsync(id);

        if (cinema == null) 
        {
            return RedirectToAction(nameof(Manage));
        }   

        return View(cinema);
    }

    [HttpPost, ActionName("SoftDeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SoftDeleteConfirmed(int id) 
    {
        if (id < 1) 
        {
            return RedirectToAction(nameof(Manage));
        }

        bool isSoftDeleted = await _cinemaService.SoftDeleteCinemaAsync(id);

        if (!isSoftDeleted) 
        {
            TempData["ErrorMessage"] = "Unable to delete cinema. It may have active movies associated with it.";

            return RedirectToAction(nameof(SoftDelete), new { id = id });
        }

        return RedirectToAction(nameof(Manage));
    }

}
