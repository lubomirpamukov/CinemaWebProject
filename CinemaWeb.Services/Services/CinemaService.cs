

using CinemaWeb.Data.Models;
using CinemaWeb.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class CinemaService(CinemaDbContext context) : ICinemaService
{
    private readonly CinemaDbContext _context = context;

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
        var viewModel = await _context.Cinemas.Select(c => new CinemaIndexViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Location = c.Location
        }).ToListAsync();

        return viewModel;
    }
}
