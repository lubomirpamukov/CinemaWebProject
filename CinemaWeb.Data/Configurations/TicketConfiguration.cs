using CinemaWeb.Data.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaWeb.Data.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        //Primary key configuration
        builder
            .HasKey(t => t.Id);

        //Configure relationship with Cinema
        builder
            .HasOne(ticket => ticket.Cinema)
            .WithMany(cinema => cinema.Tickets)
            .HasForeignKey(ticket => ticket.CinemaId)
            .OnDelete(DeleteBehavior.Cascade);

        //Configure relationship with User
        builder
            .HasOne(ticket => ticket.User)
            .WithMany(user => user.Tickets)
            .HasForeignKey(ticket => ticket.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //Configure relationship with Movie
        builder
            .HasOne(ticket => ticket.Movie)
            .WithMany(movie => movie.Tickets)
            .HasForeignKey(ticket => ticket.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
