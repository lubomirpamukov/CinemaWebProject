using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebProject.Models;

public class CinemaDbContext : IdentityDbContext<ApplicationUser>
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Cinema> Cinemas { get; set; }
    public virtual DbSet<CinemaMovie> CinemasMovies { get; set; }
    public virtual DbSet<UserMovie> UsersMovies { get; set; }


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
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CinemaMovie>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.CinemaMovies)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        //Define the composite key for UserMovie
        modelBuilder.Entity<UserMovie>()
            .HasKey(um => new { um.MovieId, um.UserId });

        //Configure relation between UserMovie and IdentityUser
        modelBuilder.Entity<UserMovie>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //Configure the realtion between UserMovie and Movie
        modelBuilder.Entity<UserMovie>()
            .HasOne(x => x.Movie)
            .WithMany()
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
