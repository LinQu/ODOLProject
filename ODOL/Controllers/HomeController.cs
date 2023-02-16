using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;
using System.Diagnostics;

namespace ODOL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public object Session { get; private set; }

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string? NIM, string? Role, string? Nama,string? Prodi)
        {
            if (Role != "" && NIM != "" && Nama != "" && Prodi != "")
            {
                ViewBag.Nama = Nama;
                ViewBag.Role = Role;
                ViewBag.NIM = NIM;
                ViewBag.Prodi = Prodi;
                HttpContext.Session.SetString("Nama", Nama);
                HttpContext.Session.SetString("Role", Role);
                HttpContext.Session.SetString("NIM", NIM);
                HttpContext.Session.SetString("Prodi", Prodi);
                
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {

                TempData["Notifikasi"] = "Username atau Password Salah";
                TempData["Icon"] = "error";
                return View();
            }


        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Pengguna pengguna)
        {
            try
            {
                var status = "Aktif";
                var login = _db.Pengguna.Where(x => x.Username.Equals(pengguna.Username) && x.Status.Equals(status)).FirstOrDefault();

                if (login != null)
                {
                    bool verif = BCrypt.Net.BCrypt.Verify(pengguna.Password, login.Password); //verify password dari hash
                    if (verif)
                    {
                        HttpContext.Session.SetString("Nama", login.Nama);
                        HttpContext.Session.SetString("Role", login.Role);
                        HttpContext.Session.SetString("Username", login.Username);
                        HttpContext.Session.SetString("Id", login.Id.ToString());
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["Notifikasi"] = "Password Salah";
                        TempData["Icon"] = "error";
                        return View();
                    }
                }
                else
                {
                    TempData["Notifikasi"] = "Username atau Password Salah";
                    TempData["Icon"] = "error";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["Notifikasi"] = ex.Message;

            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Nama");
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}