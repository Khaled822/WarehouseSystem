using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly WarehouseRepository _repo;

        public AccountController(WarehouseRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("GebruikerNaam") != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var gebruiker = _repo.Login(model.Email, model.Wachtwoord);
            if (gebruiker != null)
            {
                HttpContext.Session.SetString("GebruikerNaam", gebruiker.Naam!);
                HttpContext.Session.SetString("GebruikerEmail", gebruiker.Email!);
                HttpContext.Session.SetInt32("GebruikerId", gebruiker.Id);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Fout = "Ongeldig e-mailadres of wachtwoord.";
            return View(model);
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("GebruikerNaam") != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (model.Wachtwoord != model.WachtwoordBevestig)
            {
                ViewBag.Fout = "Wachtwoorden komen niet overeen.";
                return View(model);
            }
            if (_repo.EmailBestaat(model.Email))
            {
                ViewBag.Fout = "Dit e-mailadres is al in gebruik.";
                return View(model);
            }
            _repo.RegistreerGebruiker(model.Naam, model.Email, model.Wachtwoord);
            TempData["Success"] = "✅ Account aangemaakt! Je kunt nu inloggen.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}