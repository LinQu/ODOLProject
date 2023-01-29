using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;

namespace ODOL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ProfileController(ApplicationDBContext db)
        {
            _db = db;
        }
        
        private List<LogBook> GetLogBookBy(string? NIM)
        {
            var riwayat = _db.LogBook.Where(f => f.NIM == NIM).ToList();
            if (riwayat.Count > 0)
            {
                return riwayat;
            }
            else
            {
                return null;
            }
        }
        public IActionResult Index(string? NIM)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (NIM != null)
                {
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    ViewBag.NIM = HttpContext.Session.GetString("NIM");
                    ViewBag.Prodi = HttpContext.Session.GetString("Prodi");
                    ViewBag.LogBook = GetLogBookBy(NIM);
                    return View(_db.Mahasiswa.Where(f => f.NIM == NIM).FirstOrDefault());
                }
                else
                {
                    TempData["Notifikasi"] = "Data Tidak Ditemukan";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Index", "Dashboard");
                }

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
