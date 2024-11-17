using System.ComponentModel.DataAnnotations;
using static CinemaWeb.Common.Constants.Cinema;

namespace CinemaWeb.ViewModels.ViewModels.Cinema;

public class EditCinemaFormModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(CinemaMinLength)]
    [MaxLength(CinemaMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(LocationMinLength)]
    [MaxLength(LocationMaxLength)]
    public string Location { get; set; } = null!;
}
