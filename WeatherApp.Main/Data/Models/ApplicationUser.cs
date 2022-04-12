using Microsoft.AspNetCore.Identity;

namespace WeatherApp.Main.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<UserSelectedCity> UserSelectedCities { get; set; } = new List<UserSelectedCity>();

}
