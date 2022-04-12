using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WeatherApp.Main.Authorization;
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
        SeedAdminUser(builder);
    }

    private static void SeedAdminUser(ModelBuilder builder)
    {
        var ADMIN_ID = "c9667285-bdf7-471c-9dcc-5d916a957596";
        var ROLE_ID = ADMIN_ID;
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = Constants.Administrator,
            NormalizedName = Constants.Administrator.ToUpper()
        });

        var hasher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = ADMIN_ID,
            UserName = "admin@admin.admin",
            NormalizedUserName = "ADMIN@ADMIN.ADMIN",
            Email = "admin@admin.admin",
            NormalizedEmail = "ADMIN@ADMIN.ADMIN",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "admin@admin.admin"),
            SecurityStamp = string.Empty
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });
    }
}
