using Microsoft.AspNetCore.Mvc;

namespace WarehouseSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("GebruikerNaam") == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Naam = HttpContext.Session.GetString("GebruikerNaam");
            ViewBag.Email = HttpContext.Session.GetString("GebruikerEmail");
            return View();
        }
    }
}
