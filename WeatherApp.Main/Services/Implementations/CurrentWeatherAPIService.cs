using System.Text.Json.Serialization;
using System.Web;

using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Services.Implementations;

public class CurrentWeatherAPIService : ICurrentWeatherAPIService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public CurrentWeatherAPIService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<CurrentWeather> GetCurrentWeather(string cityName)
    {
        try
        {
            var requestUri = $"?city={HttpUtility.UrlEncode(cityName)}&key={_config["APIKey"]}";
            var response = await _http.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var weatherData = await response.Content.ReadFromJsonAsync<CurrentWeatherAPIModel>();
            if (weatherData?.CurrentWeathers?[0] is null)
                throw new CurrentWeatherNullException(cityName);

            var currentWeather = weatherData.CurrentWeathers[0];
            return new()
            {
                Description = currentWeather.Weather.Description,
                LastUpdated = DateTime.UtcNow,
                TempC = currentWeather.TempC
            };
        }
        catch (CurrentWeatherNullException ex)
        {
            // TODO
            throw;
        }
        catch (HttpRequestException ex)
        {
            // TODO
            throw;
        }
        catch (TaskCanceledException ex)
        {
            // TODO
            throw;
        }
    }

    private class CurrentWeatherNullException : Exception
    {
        public CurrentWeatherNullException(string cityName) :
            base($"Current weather could not be found for city '{cityName}'.")
        {
        }
    }

    private class CurrentWeatherAPIModel
    {
        [JsonPropertyName("data")]
        public List<Current> CurrentWeathers { get; set; } = new();
    }

    private class Current
    {
        [JsonPropertyName("city_name")]
        public string CityName { get; set; } = string.Empty;
        [JsonPropertyName("weather")]
        public Weather Weather { get; set; } = new();
        [JsonPropertyName("temp")]
        public double TempC { get; set; }
    }

    private class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }

}
