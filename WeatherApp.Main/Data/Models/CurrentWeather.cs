using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Main.Data.Models;

public class CurrentWeather
{
    public int Id { get; set; }
    public DateTime LastUpdated { get; set; }

    [MaxLength(128)]
    public string Description { get; set; } = string.Empty;
    public double TempC { get; set; }

    public int CityId { get; set; }
    public City? City { get; set; }
}
