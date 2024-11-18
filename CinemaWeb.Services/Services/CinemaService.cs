using CinemaWeb.Data;
using CinemaWeb.Infrastructure.Repository.Interfaces;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using CinemaWeb.ViewModels.ViewModels.Cinema;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class CinemaService(IRepository<Cinema> cinemaRepository) : ICinemaService
{
    private readonly IRepository<Cinema> _cinema = cinemaRepository;

    public async Task<bool> AddCinemaAsync(CinemaCreateViewModel viewModel)
    {
        var modelToAdd = new Cinema
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Location = viewModel.Location
        };

        if (modelToAdd == null) 
        {
            return false;
        }

        return await _cinema.AddAsync(modelToAdd);
    }

    public async Task<IEnumerable<CinemaIndexViewModel>> GetAllAsync()
    {
        var cinemas = await _cinema
            .GetAllAttachedAsync()
            .Where(c => c.IsDeleted == false)
            .ToArrayAsync();
        
        var viewModel = cinemas.Select(c => new CinemaIndexViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Location = c.Location
        }).ToList();

        return viewModel;
    }

	public async Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync()
	{
        var cinemasOrderByLocation = await GetAllAsync();
        return cinemasOrderByLocation.OrderBy(c => c.Location);
	}

	public async Task<CinemaDetailsViewModel> GetCinemaDetailsByIdAsync(int id) 
    {
        CinemaDetailsViewModel? cinemaViewMoviesViewModel = await _cinema
            .GetAllAttachedAsync()
            .Where(cinema => cinema.Id == id) // filter only the needed movie
            .Include(cinema => cinema.CinemaMovies) // include the mapping table in the cinema model
            .ThenInclude(cinemaMovie => cinemaMovie.Movie) // here were in the mapping table and include each movie
            .Select(cinema => new CinemaDetailsViewModel // making a projection and mapping data from the
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



        return cinemaViewMoviesViewModel;
    }


	public async Task<bool> UpdateCinemaAsync(EditCinemaFormModel model)
	{
        var cinema = await _cinema
            .GetAllAttachedAsync()
            .FirstOrDefaultAsync(c => c.Id == model.Id);

        if (cinema == null)
        {
            return false;
        }

        cinema.Name = model.Name;
        cinema.Location = model.Location;
        await _cinema.UpdateAsync(cinema);
        return true;
	}

	public async Task<EditCinemaFormModel?> GetCinemaEditModelByIdAsync(int id)
	{
		var cinema = await _cinema
            .GetAllAttachedAsync()
            .Where(c => c.Id == id)
            .Select(cinema => new EditCinemaFormModel 
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Location = cinema.Location,

			})
            .FirstOrDefaultAsync();

        if (cinema == null) 
        {
            return null;
        }

        return cinema;
	}


    public async Task<bool> SoftDeleteCinemaAsync(int cinemaId)
    {
        var cinemaToDelete = await _cinema
            .GetAllAttachedAsync()
            .Include(c => c.CinemaMovies)
            .FirstOrDefaultAsync(c => c.Id == cinemaId);

        if (cinemaToDelete == default || cinemaToDelete.CinemaMovies.Any())
        {
            return false;
        }

        cinemaToDelete.IsDeleted = true;
        await _cinema.UpdateAsync(cinemaToDelete);
        return true;
    }

    public async Task<CinemaDetailsViewModel?> GetViewDetailsAsync(int id)
    {
        var cinemaViewModel = await _cinema
            .GetAllAttachedAsync()
            .Where (c => c.Id == id)
            .Select(c => new CinemaDetailsViewModel 
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location
            })
            .FirstOrDefaultAsync();

        return cinemaViewModel;
    }
}
