namespace WeatherApp.Main.Data.Models;

public class UserSelectedCity
{
    public int Id { get; set; }

    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = new();

    public int CityId { get; set; }
    public City City { get; set; } = new();

    public bool IsFavorite { get; set; }
}
