using CinemaWeb.Infrastructure.Repository.Interfaces;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.ViewModels.Watchlist;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class WatchlistService(IRepository<UserMovie> usersMovies) : IWatchlistService
{
    private readonly IRepository<UserMovie> _usersMovies = usersMovies;
    public async Task<bool> AddToWatchlistAsync(string userId, int movieId)
    {
        var movieToAdd = await _usersMovies
            .GetAllAttachedAsync()
            .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

        if (movieToAdd == null)
        {
            UserMovie userMovieToAdd = new UserMovie
            {
                MovieId = movieId,
                UserId = userId,
            };

            return await _usersMovies.AddAsync(userMovieToAdd);
        }

        return false;
    }

    public async Task<IEnumerable<WatchListViewModel>> GetWatchlistAsync(string userId)
    {
        var watchlistMovies = await _usersMovies
                .GetAllAttachedAsync()
                .Where(um => um.UserId == userId)
                .Include(um => um.Movie)
                .Select(x => new WatchListViewModel
                {
                    Title = x.Movie.Title,
                    Genre = x.Movie.Genre!,
                    ImageUrl = x.Movie.ImageUrl,
                    MovieId = x.MovieId,
                    ReleaseDate = x.Movie.ReleaseDate.ToString("yyyy-MM-dd")
                }).ToArrayAsync();

        return watchlistMovies;
    }

    public async Task<bool> RemoveFromWatchlistAsync(string userId, int movieId)
    {
        var movieToRemove = _usersMovies
                .GetAllAttachedAsync()
                .FirstOrDefault(um => um.MovieId == movieId && um.UserId == userId);

        if (movieToRemove == null)
        {
            throw new ArgumentNullException(nameof(movieToRemove), "No movie to remove or user lack authority to remove movie");
        }

        return await _usersMovies.DeleteAsync(movieToRemove);
    }
}
