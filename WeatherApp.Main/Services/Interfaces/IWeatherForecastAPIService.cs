using WeatherApp.Main.Data.Models;

namespace WeatherApp.Main.Services.Interfaces;

public interface IWeatherForecastAPIService
{
    Task<List<WeatherForecast>> GetWeatherForecasts(string cityName, string country);
}