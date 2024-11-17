using CinemaWeb.Data.Models.Models;
using System.ComponentModel.DataAnnotations;
using static CinemaWeb.Common.Constants.Cinema;
namespace CinemaWeb.Models;

public class Cinema
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(CinemaMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(LocationMaxLength)]
    public string Location { get; set; } = null!;

    [Required]
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
        = new HashSet<CinemaMovie>();
}
