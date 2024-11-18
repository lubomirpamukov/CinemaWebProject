using CinemaWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaWeb.Data.Configurations;

public  class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
{
    public void Configure(EntityTypeBuilder<CinemaMovie> builder)
    {
        //Composite key configuration
        builder.HasKey(cm => new { cm.CinemaId, cm.MovieId });

        //Configure relationship with cinema (Cascade Delete)
        builder
            .HasOne(cm => cm.Cinema)
            .WithMany(cm => cm.CinemaMovies)
            .HasForeignKey(cm => cm.CinemaId)
            .OnDelete(DeleteBehavior.Cascade);

        //Configure relationship with movie (Restrict Delete)
        builder
            .HasOne(cm => cm.Movie)
            .WithMany(cm => cm.CinemaMovies)
            .HasForeignKey(cm => cm.MovieId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
