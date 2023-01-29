using Microsoft.AspNetCore.Mvc;

namespace ODOL.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                ViewBag.NIM = HttpContext.Session.GetString("NIM");
                return View();
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}
