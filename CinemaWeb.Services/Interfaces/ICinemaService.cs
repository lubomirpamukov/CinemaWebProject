using CinemaWeb.ViewModels.Cinema;

namespace CinemaWeb.Services.Interfaces
{
    public interface ICinemaService
    {
        public Task<IEnumerable<CinemaIndexViewModel>> GetAllAsync();
    }
}
