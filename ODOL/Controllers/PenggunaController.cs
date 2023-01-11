using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using ODOL.Models;
using ODOL.Data;

namespace ODOL.Controllers
{
    public class PenggunaController : Controller
    {
        private readonly ApplicationDBContext _db;



        public PenggunaController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View(_db.Pengguna.Where(f => f.Status == "Aktif").ToList());
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

        public async Task<IActionResult> Tambah([FromForm] Pengguna pengguna)
        {

            if (HttpContext.Session.GetString("Nama") != null)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        pengguna.CreateDate = DateTime.Now;
                        pengguna.CreateBy = HttpContext.Session.GetString("Id");
                        pengguna.Status = "Aktif";
                        await _db.Pengguna.AddAsync(pengguna);
                        await _db.SaveChangesAsync();
                        TempData["Notifikasi"] = "Data Berhasil Dibuat";
                        TempData["Icon"] = "success";
                        return RedirectToAction("index");

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        TempData["Notifikasi"] = "Data Gagal Dibuat";
                        TempData["icon"] = "error";

                    }
                }
                return View(pengguna);
                
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }

        }



        public IActionResult Ubah(int id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                var pengguna = _db.Pengguna.Find(id);
                return View(pengguna);
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ubah([FromForm] Pengguna pengguna)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        pengguna.ModifDate = DateTime.Now;
                        pengguna.ModifBy = HttpContext.Session.GetString("Id");
                        pengguna.Status = "Aktif";

                        _db.Pengguna.Update(pengguna);
                        await _db.SaveChangesAsync();
                        TempData["Notifikasi"] = "Data Berhasil Diubah";
                        TempData["Icon"] = "success";
                        return RedirectToAction("index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        TempData["Notifikasi"] = "Data Gagal Diubah\n" + ex.Message;
                        TempData["icon"] = "error";
                        return RedirectToAction("index");
                    }
                }
                return View(pengguna);

            }
            else
            {

                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("index");
        }


        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                try
                {
                    var pengguna = _db.Pengguna.Find(id);
                    pengguna.Status = "Tidak Aktif";

                    _db.Pengguna.Update(pengguna);
                    await _db.SaveChangesAsync();
                    TempData["Notifikasi"] = "Data Berhasil Dihapus";
                    TempData["Icon"] = "success";
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    TempData["Notifikasi"] = "Data Gagal Dihapus";
                    TempData["icon"] = "error";
                    return RedirectToAction("index");
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
