using WeatherApp.Main.Data.Models;

namespace WeatherApp.Main.Services.Interfaces;

public interface IWeatherService
{
    Task<CurrentWeather> GetUpdatedCurrentWeather(City city);
    Task<List<WeatherForecast>> GetUpdatedWeatherForecasts(City city);
    Task UpdateAllForecasts();
}