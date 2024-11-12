namespace CinemaWeb.Models;

public class CinemaMovie
{
    public int CinemaId { get; set; }

    public virtual Cinema Cinema { get; set; } = null!;

    public int MovieId { get; set; }

    public virtual Movie Movie { get;   set; } = null!;
}