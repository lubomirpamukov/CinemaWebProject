using System.ComponentModel.DataAnnotations;
using static CinemaWebProject.Common.Constants.Cinema;
namespace CinemaWebProject.ViewModels.Cinema;

public class CinemaCreateViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = CinemaError)]
    [MaxLength(CinemaMaxLength, ErrorMessage = CinemaMaxLengthErrorMessage)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = CinemaError)]
    [StringLength(LocationMaxLength, ErrorMessage = LocationMaxLengthErrorMessage)]
    public string Location { get; set; } = null!;
}
