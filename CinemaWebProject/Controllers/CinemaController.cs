using CinemaWeb.Data.Models;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Controllers;

public class CinemaController(CinemaDbContext context, ICinemaService cinemaService) : Controller
{
    private readonly CinemaDbContext _context = context;
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
        CinemaViewMovieProgramViewModel? cinemaViewMoviesViewModel = await _context.Cinemas
            .Where(cinema => cinema.Id == id) // filter only the needed movie
            .Include(cinema => cinema.CinemaMovies) // include the mapping table in the cinema model
            .ThenInclude(cinemaMovie => cinemaMovie.Movie) // here were in the mapping table and include each movie
            .Select(cinema => new CinemaViewMovieProgramViewModel // making a projection and mapping data from the
            {                                                        //database model to the viewModel
                Id = cinema.Id,
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies.Select(cinemaMovie => new MovieProgramViewModel
                { // filling up the movie program for the cinema
                    Title = cinemaMovie.Movie.Title, // from the cinema movie mapping table in cinema i select the movie and take the title
                    Duration = cinemaMovie.Movie.Duration // // from the cinema movie mapping table in cinema i select the movie and take the title
                }).ToList()
            }).FirstOrDefaultAsync();

        if (cinemaViewMoviesViewModel == null)
        {
            return RedirectToAction("Index");
        }

        return View(cinemaViewMoviesViewModel);
    } 
}
