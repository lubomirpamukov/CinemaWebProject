using Microsoft.AspNetCore.Identity;

namespace CinemaWebProject.Models;

public class UserMovie
{
    public string UserId { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
