using CinemaWeb.Data;
using CinemaWeb.Infrastructure.Repository.Interfaces;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using CinemaWeb.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class CinemaService(IRepository<Cinema> cinemaRepository) : ICinemaService
{
    private readonly IRepository<Cinema> _cinema = cinemaRepository;

    public async Task<bool> CreateAsync(CinemaCreateViewModel viewModel)
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

        await _context.Cinemas.AddAsync(modelToAdd);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CinemaIndexViewModel>> GetAllAsync()
    {
        var cinemas = await _cinema.GetAllAsync();
        
        var viewModel = cinemas.Select(c => new CinemaIndexViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Location = c.Location
        }).ToList();

        return viewModel;
    }

    public async Task<CinemaViewMovieProgramViewModel> ViewMovieProgramAsync(int id) 
    {
        CinemaViewMovieProgramViewModel? cinemaViewMoviesViewModel = await _cinema
            .GetAllAttachedAsync()
            .Where(cinema => cinema.Id == id) // filter only the needed movie
            .Include(cinema => cinema.CinemaMovies) // include the mapping table in the cinema model
            .ThenInclude(cinemaMovie => cinemaMovie.Movie) // here were in the mapping table and include each movie
            .Select(cinema => new CinemaViewMovieProgramViewModel // making a projection and mapping data from the
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
}
