using CinemaWeb.Data;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.ViewModels.Watchlist;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebProject.Controllers
{
	public class WatchlistController(
		IWatchlistService watchlistService,
		CinemaDbContext context,
		UserManager<ApplicationUser> userManager
		): Controller
	{
		private readonly IWatchlistService _watchlistService = watchlistService;
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly CinemaDbContext _context = context;
		public async Task<IActionResult> Index()
		{
			var userId = _userManager.GetUserId(User);
			if (userId == null)
			{ 
				throw new ArgumentNullException(nameof(userId), "Null or invalid user");
			}

			var result = await _watchlistService.GetWatchlistAsync(userId);

			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddToWatchList(int movieId) 
		{
			var userId = _userManager.GetUserId(User);

			if(userId == null) 
			{
				throw new ArgumentNullException(nameof(userId));
			}

			var result = await _watchlistService.AddToWatchlistAsync(userId, movieId);

			return RedirectToAction("Index", "Movie");
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromWatchlist(int movieId) 
		{
			var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var result = await _watchlistService.RemoveFromWatchlistAsync(userId, movieId);

			return RedirectToAction("Index");
		}
    }
}
