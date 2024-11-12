namespace CinemaWeb.ViewModels.Cinema;

public class CinemaCheckBoxItemViewModel
{
    //no validations here , becouse we dont have user input

    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsSelected { get; set; }
}
