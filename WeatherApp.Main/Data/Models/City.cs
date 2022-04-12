using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Main.Data.Models;

public class City
{
    public int Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; } = string.Empty;

    public CurrentWeather? CurrentWeather { get; set; }

    public ICollection<WeatherForecast> Forecasts { get; set; } = new List<WeatherForecast>();
    public ICollection<UserSelectedCity> UserSelectedCities { get; set; } = new List<UserSelectedCity>();

}
