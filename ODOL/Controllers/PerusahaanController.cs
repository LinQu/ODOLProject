using Microsoft.AspNetCore.Mvc;

using ODOL.Models;
using ODOL.Data;

namespace ODOL.Controllers
{
    public class PerusahaanController : Controller
    {
        private readonly ApplicationDBContext _db;



        public PerusahaanController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public JsonResult GetAllPerusahaan()
        {
            {
                var perusahaan = _db.Perusahaan.ToList();
                return Json(new { data = perusahaan });
            }
        }

        [HttpGet]
        public JsonResult SearchPeru(int search)
        {
            var perusahaan = _db.Perusahaan.Where(f => f.Id == search).ToList();
            return Json(new { data = perusahaan });
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View(_db.Perusahaan.Where(f => f.Status == "Aktif")); 
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Tambah()
        {

            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View();
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Tambah([FromForm] Perusahaan perusahaan)
        {

            if (HttpContext.Session.GetString("Nama") != null)
            {

                if (ModelState.IsValid)
                {
                    perusahaan.Status = "Aktif";
                    _db.Perusahaan.Add(perusahaan);
                    await _db.SaveChangesAsync();
                    TempData["Notifikasi"] = "Data Berhasil Ditambahkan";
                    TempData["Icon"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Notifikasi"] = "Data Gagal Ditambahkan";
                    TempData["Icon"] = "error";
                    return View();
                }
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Detail(int id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View(_db.Perusahaan.Where(f => f.Id == id).FirstOrDefault());
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
