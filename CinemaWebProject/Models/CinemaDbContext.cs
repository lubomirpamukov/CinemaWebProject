using Microsoft.EntityFrameworkCore;

namespace CinemaWebProject.Models;

public class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {

    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<CinemaMovie> CinemasMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Mapping table configuration
        //Composite key
        modelBuilder.Entity<CinemaMovie>()
            .HasKey(cm => new { cm.MovieId, cm.CinemaId });

        modelBuilder.Entity<CinemaMovie>()
            .HasOne(x => x.Cinema)
            .WithMany(x => x.CinemaMovies)
            .HasForeignKey(x => x.CinemaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CinemaMovie>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.CinemaMovies)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
