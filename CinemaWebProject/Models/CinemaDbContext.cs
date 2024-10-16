using Microsoft.EntityFrameworkCore;

namespace CinemaWebProject.Models;

public class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {

    }

    public DbSet<Movie> Movies { get; set; }
}
