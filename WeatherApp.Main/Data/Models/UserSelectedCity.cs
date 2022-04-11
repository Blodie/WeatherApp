namespace WeatherApp.Main.Data.Models;

public class UserSelectedCity
{
    public int Id { get; set; }

    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    public bool IsFavorite { get; set; }
}
