using CinemaWeb.Data;
using CinemaWeb.Models;
using CinemaWeb.ViewModels.ViewModels.Watchlist;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebProject.Controllers
{
	public class WatchlistController(
		CinemaDbContext context,
		UserManager<ApplicationUser> userManager)
		: Controller
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly CinemaDbContext _context = context;
		public async Task<IActionResult> Index()
		{
			var userId = _userManager.GetUserId(User);

			var watchlistMovies = await _context.UsersMovies
				.Where(um => um.UserId == userId)
				.Include(um => um.Movie)
				.Select(x => new WatchListViewModel 
				{
					Title = x.Movie.Title,
					Genre = x.Movie.Genre,
					ImageUrl = x.Movie.ImageUrl,
					MovieId = x.MovieId,
					ReleaseDate = x.Movie.ReleaseDate.ToString("yyyy-MM-dd")
				}).ToArrayAsync();

			return View(watchlistMovies);
		}

		[HttpPost]
		public async Task<IActionResult> AddToWatchList(int movieId) 
		{
			var userId = _userManager.GetUserId(User);

			if(userId == null) 
			{
				throw new ArgumentNullException(nameof(userId));
			}

			var movieToAdd = await _context.UsersMovies
				.FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

			if (movieToAdd == null)
			{
                UserMovie userMovieToAdd = new UserMovie
                {
                    MovieId = movieId,
                    UserId = userId,
                };

                await _context.UsersMovies.AddAsync(userMovieToAdd);
                await _context.SaveChangesAsync();
            }

			return RedirectToAction("Index", "Movie");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromWatchlist(int movieId) 
		{
			var userId = _userManager.GetUserId(User);

			var movieToRemove = _context.UsersMovies
				.FirstOrDefault(um => um.MovieId == movieId && um.UserId == userId);

			if (movieToRemove == null)
			{ 
				throw new ArgumentNullException(nameof(movieToRemove), "No movie to remove or user lack authority to remove movie");
			}

			_context.UsersMovies.Remove(movieToRemove);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
    }
}
