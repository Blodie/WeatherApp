using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WeatherApp.Main.Authorization;
using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Controllers;

[Authorize(Roles = Constants.Administrator)]
public class OthersCitiesController : Controller
{
    private readonly IUserService _userService;

    public OthersCitiesController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ActionResult> Index()
    {
        var users = await _userService.GetUsers();
        return View(users);
    }

    [HttpPost]
    public async Task DeselectCityForUser([FromBody] UserSelectedCity userSelectedCity)
    {
        await _userService.DeselectCity(userSelectedCity.CityId.Value, userSelectedCity.ApplicationUserId);
    }

}
