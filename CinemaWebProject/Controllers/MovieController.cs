using Microsoft.AspNetCore.Mvc;
using CinemaWebProject.Models;
using CinemaWebProject.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebProject.Controllers;

public class MovieController(CinemaDbContext context) : Controller
{
    private readonly CinemaDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        var viewModel = new List<IndexViewModel>();
        var movies = await _context.Movies.ToListAsync();

        foreach (var movie in movies) 
        {
            var movieView = new IndexViewModel
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateViewModel movie)
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
            DetailsViewModel viewModel = new DetailsViewModel 
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


}
