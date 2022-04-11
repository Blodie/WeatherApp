using System.Text.Json.Serialization;
using System.Web;

using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Services.Implementations;

public class WeatherForecastAPIService : IWeatherForecastAPIService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public WeatherForecastAPIService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<List<WeatherForecast>> GetWeatherForecasts(string cityName)
    {
        try
        {
            var requestUri = $"?city={HttpUtility.UrlEncode(cityName)}&key={_config["APIKey"]}";
            var response = await _http.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var forecastData = await response.Content.ReadFromJsonAsync<WeatherForecastAPIModel>();
            if (forecastData?.Forecasts is null)
                throw new WeatherForecastsNullException(cityName);

            return forecastData.Forecasts.Select(forecast => new WeatherForecast
            {
                ValidDate = forecast.ValidDate,
                Description = forecast.Weather.Description,
                AvgTempC = forecast.AvgTempC,
                MinTempC = forecast.MinTempC,
                MaxTempC = forecast.MaxTempC,
            }).ToList();
        }
        catch (WeatherForecastsNullException ex)
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

    private class WeatherForecastsNullException : Exception
    {
        public WeatherForecastsNullException(string cityName) :
            base($"Weather forecasts could not be found for city '{cityName}'.")
        {
        }
    }

    private class WeatherForecastAPIModel
    {
        [JsonPropertyName("data")]
        public List<Forecast> Forecasts { get; set; } = new();
        [JsonPropertyName("city_name")]
        public string CityName { get; set; } = string.Empty;
    }

    private class Forecast
    {
        [JsonPropertyName("valid_date")]
        public DateTime ValidDate { get; set; }
        [JsonPropertyName("weather")]
        public Weather Weather { get; set; } = new();
        [JsonPropertyName("max_temp")]
        public double MaxTempC { get; set; }
        [JsonPropertyName("min_temp")]
        public double MinTempC { get; set; }
        [JsonPropertyName("temp")]
        public double AvgTempC { get; set; }
    }

    private class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
