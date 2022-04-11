using WeatherApp.Main.ViewModels;

namespace WeatherApp.Main.Services.Interfaces;

public interface IUserService
{
    Task DeselectCity(int cityId, string userId);
    Task<List<WeatherViewModel>> GetCitiesWeatherData(string? userId = null);
    Task SelectCity(string cityName, string userId);
    Task ToggleFavorite(int cityId, string userId);
}