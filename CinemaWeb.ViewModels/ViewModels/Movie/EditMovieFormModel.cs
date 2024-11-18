using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaWeb.Common.Constants.Movie
namespace CinemaWeb.ViewModels.ViewModels.Movie;



public class EditMovieFormModel //: IMapFrom<Movie>, IHaveCustomMappings

{

    public int Id { get; set; }



    [Required(ErrorMessage = TitleError)]

    [MaxLength(TitleMaxLength)]

    public string Title { get; set; } = null!;



    [Required(ErrorMessage = GenreError)]

    [MinLength(GenreMinLength)]

    [MaxLength(GenreMaxLength)]

    public string Genre { get; set; } = null!;



    [Required(ErrorMessage = ReleaseDateError)]

    [DataType(DataType.Date)]

    public DateTime ReleaseDate { get; set; }



    [Required(ErrorMessage = DurationError)]

    [Range(DurationMinLength, DurationMaxLength)]

    public int Duration { get; set; }



    [Required(ErrorMessage = DirectorError)]

    [MinLength(DirectorMinLength)]

    [MaxLength(DirectorMaxLength)]

    public string Director { get; set; } = null!;



    [Required]

    [MinLength(DescriptionMinLength)]

    [MaxLength(DescriptionMaxLength)]

    public string Description { get; set; } = null!;



    [MaxLength(ImageUrlMaxLength)]

    public string? ImageUrl { get; set; }



   /* public void CreateMappings(IProfileExpression configuration)

    {

        configuration.CreateMap<Movie, EditMovieFormModel>()

            .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(s => s.ReleaseDate));

    }*/

}

}