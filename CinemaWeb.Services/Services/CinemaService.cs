

using CinemaWeb.Data.Models;
using CinemaWeb.Services.Interfaces;
using CinemaWeb.ViewModels.Cinema;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services.Services;

public class CinemaService(CinemaDbContext context) : ICinemaService
{
    private readonly CinemaDbContext _context = context;
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
