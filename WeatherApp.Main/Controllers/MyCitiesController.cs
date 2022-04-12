using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WeatherApp.Main.Data.Models;
using WeatherApp.Main.Services.Interfaces;

namespace WeatherApp.Main.Controllers;

[Authorize]
public class MyCitiesController : Controller
{
    private readonly IUserService _userService;

    public MyCitiesController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var selectedCitiesWeatherData = await _userService.GetCitiesWeatherData(userId);
        return View(selectedCitiesWeatherData);
    }

    [HttpPost]
    public async Task SelectCity([FromBody] City city)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userService.SelectCity(city.Name, userId);
    }

    [HttpPost]
    public async Task DeselectCity([FromBody] City city)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userService.DeselectCity(city.Id, userId);
    }

    [HttpPost]
    public async Task ToggleFavoriteCity([FromBody] City city)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userService.ToggleFavorite(city.Id, userId);
    }
}
