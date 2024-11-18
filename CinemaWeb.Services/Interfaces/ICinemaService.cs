using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.ViewModels.Cinema;

namespace CinemaWeb.Services.Interfaces
{
    public interface ICinemaService
    {
        public Task<IEnumerable<CinemaIndexViewModel>> GetAllAsync();

        public Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync();

        public Task<bool> AddCinemaAsync(CinemaCreateViewModel viewModel);

        public Task<CinemaDetailsViewModel> GetCinemaDetailsByIdAsync(int id);

        public Task<CinemaDetailsViewModel?> GetViewDetailsAsync(int id);

        public Task<EditCinemaFormModel?> GetCinemaEditModelByIdAsync(int id);

        public Task<bool> UpdateCinemaAsync(EditCinemaFormModel model);

        public Task<bool> SoftDeleteCinemaAsync(int id);
    }
}
