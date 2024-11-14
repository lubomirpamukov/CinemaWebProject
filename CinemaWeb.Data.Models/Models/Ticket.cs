using CinemaWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Data.Models.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    public int CinemaId { get; set; }

    [Required]
    [ForeignKey(nameof(CinemaId))]
    public virtual Cinema Cinema { get; set; } = null!;
    public int MovieId { get; set; }

    [Required]
    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;
    public string UserId { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

}
