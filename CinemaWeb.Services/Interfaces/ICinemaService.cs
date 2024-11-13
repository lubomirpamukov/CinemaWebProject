using CinemaWeb.ViewModels.Cinema;

namespace CinemaWeb.Services.Interfaces
{
    public interface ICinemaService
    {
        public Task<IEnumerable<CinemaIndexViewModel>> GetAllAsync();

        public Task<bool> CreateAsync(CinemaCreateViewModel viewModel);

        public Task<CinemaViewMovieProgramViewModel> ViewMovieProgramAsync(int id);
    }
}
