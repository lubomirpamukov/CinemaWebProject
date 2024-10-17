using System.ComponentModel.DataAnnotations;
using static CinemaWebProject.Common.Constants.Cinema;
namespace CinemaWebProject.Models;

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

    public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
        = new HashSet<CinemaMovie>();
}
