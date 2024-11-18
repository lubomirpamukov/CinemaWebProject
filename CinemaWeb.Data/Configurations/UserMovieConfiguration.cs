using CinemaWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaWeb.Data.Configurations;

public class UserMovieConfiguration : IEntityTypeConfiguration<UserMovie>
{
    public void Configure(EntityTypeBuilder<UserMovie> builder)
    {
        //Configure composite Key
        builder
            .HasKey(um => new { um.UserId, um.MovieId });

        //Configure relationship with User
        builder
            .HasOne(um => um.User)
            .WithMany(um => um.UserMovies)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //Configure Relationship with Movie
        builder
            .HasOne(um => um.Movie)
            .WithMany(um => um.UsersMovies)
            .HasForeignKey(um => um.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
