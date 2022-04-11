using WeatherApp.Main.Data.Models;

namespace WeatherApp.Main.Services.Interfaces;

public interface ICurrentWeatherAPIService
{
    Task<CurrentWeather> GetCurrentWeather(string cityName, string country);
}