using Microsoft.AspNetCore.Mvc;
using CinemaWebProject.Models;
using CinemaWebProject.ViewModels.Movie;

namespace CinemaWebProject.Controllers;

public class MovieController : Controller
{
    private static List<Movie> movies = new List<Movie>();

    public IActionResult Index()
    {
        var viewModel = new List<IndexViewModel>();

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
    public IActionResult Create(CreateViewModel movie)
    {
        var moveiId = movies.Count + 1;

        if (ModelState.IsValid) 
        {
            Movie movieToAdd = new Movie 
            {
                Id = moveiId,
                Title = movie.Title,
                Description = movie.Description,
                Director = movie.Director,
                Duration = movie.Duration,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };

            movies.Add(movieToAdd);
            return RedirectToAction("Index");
        }

        return View(movie);
    }

    public IActionResult Details(int id)
    {
        Movie? movie = movies.FirstOrDefault(m => m.Id == id);

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
