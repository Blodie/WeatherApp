using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Main.Data.Models;

public class WeatherForecast
{
    public int Id { get; set; }
    public DateTime ValidDate { get; set; }

    [MaxLength(128)]
    public string Description { get; set; } = string.Empty;
    public double MinTempC { get; set; }
    public double MaxTempC { get; set; }
    public double AvgTempC { get; set; }

    public int CityId { get; set; }
    public City? City { get; set; }
}
