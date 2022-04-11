using WeatherApp.Main.Data.Models;

namespace WeatherApp.Main.ViewModels;

public class WeatherViewModel
{
    public bool IsFavorite { get; set; }
    public string? Country { get; set; }
    public string? CityName { get; set; }
    public CurrentWeather? CurrentWeather { get; set; }
    public List<WeatherForecast> WeatherForecasts { get; set; } = new();
    public int CityId { get; internal set; }
}
