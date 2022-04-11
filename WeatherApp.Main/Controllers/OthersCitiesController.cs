using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WeatherApp.Main.Authorization;

namespace WeatherApp.Main.Controllers;

[Authorize(Roles = Constants.Administrator)]
public class OthersCitiesController : Controller
{
    // GET: OthersCitiesController
    public ActionResult Index()
    {
        return View();
    }

    // GET: OthersCitiesController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: OthersCitiesController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: OthersCitiesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
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

    // GET: OthersCitiesController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: OthersCitiesController/Edit/5
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

    // GET: OthersCitiesController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: OthersCitiesController/Delete/5
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
