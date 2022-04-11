using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WeatherApp.Main.Data.Models;

namespace WeatherApp.Main.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public virtual DbSet<CurrentWeather> CurrentWeathers { get; set; }
    public virtual DbSet<UserSelectedCity> UserSelectedCities { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<City>()
            .HasOne(city => city.CurrentWeather)
            .WithOne(weather => weather.City)
            .HasForeignKey<CurrentWeather>(weather => weather.CityId);
    }
}
