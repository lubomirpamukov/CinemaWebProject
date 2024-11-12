using System.ComponentModel.DataAnnotations;
using static CinemaWeb.Common.Constants.Cinema;
namespace CinemaWeb.ViewModels.Cinema;

public class CinemaIndexViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = CinemaError)]
    [MaxLength(CinemaMaxLength, ErrorMessage = CinemaMaxLengthErrorMessage)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = CinemaError)]
    [StringLength(LocationMaxLength, ErrorMessage = LocationMaxLengthErrorMessage)]
    public string Location { get; set; } = null!;
}
