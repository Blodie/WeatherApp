using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Main.Data.Models;

public class City
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(128)]
    public string Country { get; set; } = string.Empty;

    public int CurrentWeatherId { get; set; }
    public CurrentWeather CurrentWeather { get; set; } = new();

    public ICollection<WeatherForecast> Forecasts { get; set; } = new List<WeatherForecast>();
    public ICollection<UserSelectedCity> UserSelectedCities { get; set; } = new List<UserSelectedCity>();

}
