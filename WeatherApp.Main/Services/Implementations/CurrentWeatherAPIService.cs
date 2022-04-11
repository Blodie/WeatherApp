using System.Text.Json.Serialization;
using System.Web;

using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Services.Implementations;

public class CurrentWeatherAPIService : ICurrentWeatherAPIService
{
    private readonly HttpClient _http;

    public CurrentWeatherAPIService(HttpClient http)
    {
        _http = http;
    }

    public async Task<CurrentWeather> GetCurrentWeather(string cityName, string country)
    {
        try
        {
            var request = await _http.GetAsync($"&city={HttpUtility.UrlEncode(cityName)},{HttpUtility.UrlEncode(country)}");
            request.EnsureSuccessStatusCode();
            var weatherData = await request.Content.ReadFromJsonAsync<CurrentWeatherAPIModel>();
            if (weatherData?.CurrentWeathers?[0] is null)
                throw new CurrentWeatherNullException(cityName, country);

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
        public CurrentWeatherNullException(string cityName, string country) :
            base($"Current weather could not be found for city '{cityName}' in country '{country}'.")
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
        [JsonPropertyName("country_code")]
        public string Country { get; set; } = string.Empty;
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
