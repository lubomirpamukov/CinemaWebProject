using CinemaWeb.ViewModels.ViewModels.Watchlist;

namespace CinemaWeb.Services.Interfaces;

public interface IWatchlistService
{
    // Index
    public Task<IEnumerable<WatchListViewModel>> GetWatchlistAsync(string userId);
   
    //AddToWatchList
    public Task<bool> AddToWatchlistAsync(string userId, int movieId);

    //RemoveFromWatchlist
    public Task<bool> RemoveFromWatchlistAsync(string userId,int movieId);
}
