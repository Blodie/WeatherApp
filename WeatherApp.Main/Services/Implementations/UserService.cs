using Microsoft.EntityFrameworkCore;

using WeatherApp.Main.Data;
using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;
using WeatherApp.Main.ViewModels;

namespace WeatherApp.Main.Services.Implementations;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IWeatherService _weatherService;
    private readonly int _currentWeatherUpdateFreqH;

    public UserService(ApplicationDbContext context, IConfiguration config, IWeatherService weatherService)
    {
        _context = context;
        _weatherService = weatherService;
        _currentWeatherUpdateFreqH = config.GetSection("CurrentWeatherUpdateFreqHours").Get<int>();
    }

    public async Task<List<WeatherViewModel>> GetCitiesWeatherData(string? userId = null)
    {
        if (userId is null)
            return await GetDefaultCitiesWeatherList();

        var userSelectedCities = await _context.UserSelectedCities
            .Include(userSelectedCity => userSelectedCity.City)
                .ThenInclude(city => city.CurrentWeather)
            .Include(userSelectedCity => userSelectedCity.City)
                .ThenInclude(city => city.Forecasts)
            .Where(userSelectedCity => userSelectedCity.ApplicationUserId == userId)
            .ToListAsync();

        var weatherViewModels = new List<WeatherViewModel>();

        foreach (var userSelectedCity in userSelectedCities)
        {
            if (userSelectedCity.City.CurrentWeather.LastUpdated.AddHours(_currentWeatherUpdateFreqH) < DateTime.UtcNow)
                userSelectedCity.City.CurrentWeather = await _weatherService.GetUpdatedCurrentWeather(userSelectedCity.City);

            weatherViewModels.Add(new()
            {
                CityId = userSelectedCity.CityId,
                IsFavorite = userSelectedCity.IsFavorite,
                Country = userSelectedCity.City.Country,
                CityName = userSelectedCity.City.Name,
                CurrentWeather = userSelectedCity.City.CurrentWeather,
                WeatherForecasts = userSelectedCity.City.Forecasts.ToList()
            });
        }

        return weatherViewModels;
    }

    public async Task SelectCity(string cityName, string country, string userId)
    {
        if (userId is null)
            throw new ArgumentNullException(nameof(userId));

        cityName = cityName?.Trim().Normalize();
        country = country?.Trim().Normalize();

        var city = await GetCity(cityName, country);

        _context.UserSelectedCities.Add(
            new()
            {
                CityId = city.Id,
                ApplicationUserId = userId,
            });
        await _context.SaveChangesAsync();
    }

    public async Task DeselectCity(int cityId, string userId)
    {
        var userSelectedCity = await _context.UserSelectedCities
            .FirstOrDefaultAsync(userSelectedCity => userSelectedCity.CityId == cityId && userSelectedCity.ApplicationUserId == userId);

        if (userSelectedCity is null)
            throw new UserSelectedCityNotFoundException(cityId, userId);

        _context.UserSelectedCities.Remove(userSelectedCity);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleFavorite(int cityId, string userId)
    {
        var userSelectedCity = await _context.UserSelectedCities
            .FirstOrDefaultAsync(userSelectedCity => userSelectedCity.CityId == cityId && userSelectedCity.ApplicationUserId == userId);

        if (userSelectedCity is null)
            throw new UserSelectedCityNotFoundException(cityId, userId);

        userSelectedCity.IsFavorite = !userSelectedCity.IsFavorite;
        _context.UserSelectedCities.Update(userSelectedCity);
        await _context.SaveChangesAsync();
    }

    private async Task<List<WeatherViewModel>> GetDefaultCitiesWeatherList()
    {
        //TODO
        return new();
    }

    private async Task<City> GetCity(string cityName, string country)
    {
        var city = await _context.Cities.FirstOrDefaultAsync(city => city.Name == cityName && city.Country == country);
        if (city is null)
        {
            city = new()
            {
                Name = cityName,
                Country = country
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }
        return city;
    }

    private class UserSelectedCityNotFoundException : Exception
    {
        public UserSelectedCityNotFoundException(int cityId, string userId) :
            base($"UserSelectedCity cannot be found for cityId '{cityId}' and userId '{userId}'.")
        {
        }
    }
}