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
    private readonly List<string> _defaultCities;

    public UserService(ApplicationDbContext context, IConfiguration config, IWeatherService weatherService)
    {
        _context = context;
        _weatherService = weatherService;
        _currentWeatherUpdateFreqH = config.GetSection("CurrentWeatherUpdateFreqHours").Get<int>();
        _defaultCities = config.GetSection("DefaultCities").Get<List<string>>();
    }

    public async Task<List<WeatherViewModel>> GetCitiesWeatherData(string? userId = null)
    {
        if (userId is null)
            return await GetDefaultCitiesWeatherList();

        var userSelectedCities = await _context.UserSelectedCities
            .Where(userSelectedCity => userSelectedCity.ApplicationUserId == userId)
            .Include(userSelectedCity => userSelectedCity.City)
                .ThenInclude(city => city.CurrentWeather)
            .Include(userSelectedCity => userSelectedCity.City)
                .ThenInclude(city => city.Forecasts)
            .AsNoTracking()
            .ToListAsync();

        var weatherViewModels = new List<WeatherViewModel>();

        foreach (var userSelectedCity in userSelectedCities)
        {
            if (userSelectedCity.City.CurrentWeather.LastUpdated.AddHours(_currentWeatherUpdateFreqH) < DateTime.UtcNow)
                userSelectedCity.City.CurrentWeather = await _weatherService.GetUpdatedCurrentWeather(userSelectedCity.City);

            weatherViewModels.Add(ConvertCityToWeatherViewModel(userSelectedCity.City, userSelectedCity.IsFavorite));
        }

        return weatherViewModels;
    }

    public async Task SelectCity(string cityName, string userId)
    {
        if (userId is null)
            throw new ArgumentNullException(nameof(userId));

        cityName = cityName?.Trim().Normalize();

        var city = await GetCity(cityName);

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

    public async Task<List<ApplicationUser>> GetUsers()
    {
        return await _context.Users
            .Include(userSelectedCity => userSelectedCity.UserSelectedCities)
            .ThenInclude(userSelectedCity => userSelectedCity.City)
            .Where(user => user.Email != null && user.UserSelectedCities.Count > 0)
            .ToListAsync();
    }

    private async Task<List<WeatherViewModel>> GetDefaultCitiesWeatherList()
    {
        var weatherViewModels = new List<WeatherViewModel>();
        foreach (var cityName in _defaultCities)
        {
            var city = await GetCity(cityName);
            weatherViewModels.Add(ConvertCityToWeatherViewModel(city));
        }
        return weatherViewModels;
    }

    private static WeatherViewModel ConvertCityToWeatherViewModel(City city, bool isFavorite = false)
    {
        return new()
        {
            CityId = city.Id,
            IsFavorite = isFavorite,
            CityName = city.Name,
            CurrentWeather = city.CurrentWeather,
            WeatherForecasts = city.Forecasts.Where(forecast => forecast.ValidDate > DateTime.Today).ToList()
        };
    }

    private async Task<City> GetCity(string cityName)
    {
        var city = await _context.Cities
            .Include(city => city.CurrentWeather)
            .Include(city => city.Forecasts)
            .AsNoTracking()
            .FirstOrDefaultAsync(city => city.Name == cityName);

        if (city is null)
        {
            city = new()
            {
                Name = cityName,
            };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            city.CurrentWeather = await _weatherService.GetUpdatedCurrentWeather(city);
            city.Forecasts = await _weatherService.GetUpdatedWeatherForecasts(city);
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