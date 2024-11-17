namespace CinemaWeb.Common;

public class Constants
{
    public class Movie 
    {
        public const int TitleMinLength = 2;
        public const int TitleMaxLength = 50;

        public const int GenreMinLength = 4;
        public const int GenreMaxLength = 20;

        public const int DirectorMinLength = 5;
        public const int DirectorMaxLength = 50;

        public const int DurationMinLength = 10;
        public const int DurationMaxLength = 500;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 500;

        public const string TitleError = "Movie title is required.";
        public const string TitleLengthError = "Movie title must be between 2 and 50 characters.";
        public const string GenreError = "Genre is required.";
        public const string ReleaseDateError = "Release date is required.";
        public const string DirectorError = "Director name is required.";
        public const string DirectorNameLengthError = "Director name is too long.";
        public const string DurationError = "Please specify the movie duration";
        public const string DurationRangeError = "Duration must be between 10 and 500 minutes.";
    }

    public class Cinema 
    {
        public const string CinemaError = "Cinema name is required.";
        public const string CinemaMaxLengthErrorMessage = "Cinema name must be between 1 and 80 characters.";
        public const int CinemaMaxLength = 80;
        public const int CinemaMinLength = 1;

        public const int LocationMinLength = 1;
        public const int LocationMaxLength = 50;
        public const string LocationError = "Location is required.";
        public const string LocationMaxLengthErrorMessage = "Location must be between 1 and 50 characters.";
    }
}
