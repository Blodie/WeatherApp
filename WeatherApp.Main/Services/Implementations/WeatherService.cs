using Microsoft.EntityFrameworkCore;

using WeatherApp.Main.Data;
using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Services.Implementations;

public class WeatherService : IWeatherService
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentWeatherAPIService _currentWeatherService;
    private readonly IWeatherForecastAPIService _weatherForecastService;

    public WeatherService(ApplicationDbContext context, ICurrentWeatherAPIService currentWeatherService, IWeatherForecastAPIService weatherForecastService)
    {
        _context = context;
        _currentWeatherService = currentWeatherService;
        _weatherForecastService = weatherForecastService;
    }

    public async Task<CurrentWeather> GetUpdatedCurrentWeather(City city)
    {
        var currentWeather = await _currentWeatherService.GetCurrentWeather(city.Name, city.Country);
        var outdatedCurrentWeather = city.CurrentWeather;
        currentWeather.Id = outdatedCurrentWeather.Id;
        _context.CurrentWeathers.Update(currentWeather);
        await _context.SaveChangesAsync();
        return currentWeather;
    }

    public async Task<List<WeatherForecast>> GetUpdatedWeatherForecasts(City city)
    {
        var weatherForecasts = await _weatherForecastService.GetWeatherForecasts(city.Name, city.Country);
        var outdatedWeatherForecasts = city.Forecasts;
        _context.RemoveRange(outdatedWeatherForecasts);
        _context.AddRange(weatherForecasts);
        await _context.SaveChangesAsync();
        return weatherForecasts;
    }
}
