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

    // GET: MyCities
    public async Task<ActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var selectedCitiesWeatherData = await _userService.GetCitiesWeatherData(userId);
        return View(selectedCitiesWeatherData);
    }

    // GET: MyCities/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // POST: MyCities/Create
    [HttpPost]
    public async Task SelectCity([FromBody] City city)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userService.SelectCity(city.Name, userId);

    }

    // GET: MyCities/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: MyCities/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: MyCities/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: MyCities/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
