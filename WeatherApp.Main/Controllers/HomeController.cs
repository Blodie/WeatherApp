using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using WeatherApp.Main.Services.Interfaces;
using WeatherApp.Main.ViewModels;

namespace WeatherApp.Main.Controllers;
public class HomeController : Controller
{
    private readonly IUserService _userService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var defaultCitiesWeatherData = await _userService.GetCitiesWeatherData();
        return View(defaultCitiesWeatherData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
