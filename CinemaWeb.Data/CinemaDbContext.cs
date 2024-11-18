using CinemaWeb.Data.Configurations;
using CinemaWeb.Data.Models.Models;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Data;

public class CinemaDbContext : IdentityDbContext<ApplicationUser>
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Cinema> Cinemas { get; set; }
    public virtual DbSet<CinemaMovie> CinemasMovies { get; set; }
    public virtual DbSet<UserMovie> UsersMovies { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Manually adding configuration
      /*modelBuilder.ApplyConfiguration(new CinemaMovieConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new UserMovieConfiguration())*/;

        //Adding configuration with reflection
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
