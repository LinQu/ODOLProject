using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;
using ODOL.Controllers;

namespace ODOL.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDBContext _db;

        public DashboardController(ApplicationDBContext db)
        {
            _db = db;
        }

        

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                ViewBag.NIM = HttpContext.Session.GetString("NIM");
                ViewBag.CountPeru = _db.Perusahaan.Where(f => f.Status == "Aktif").Count();
                ViewBag.CountMhs = _db.Mahasiswa.Where(f => f.Status == "Aktif").Count();
                ViewBag.CountUser = _db.Pengguna.Where(f => f.Status == "Aktif").Count();
                if (ViewBag.Role == "Pembimbing")
                {
                    var idPeng = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    var idPemb = _db.Pembimbing.Where(f => f.idPengguna == idPeng).FirstOrDefault();
                    var log = _db.LogBook.Where((f => f.idPembimbing == idPemb.id && f.Status == "Belum Disetujui")).Count();
                    ViewBag.Approve = log;
                }
                if(ViewBag.Role == "MAHASISWA")
                {
                    var tanggal = DateTime.Now.Date;
                    var log = _db.LogBook.OrderBy(f => f.Tanggal).LastOrDefault(f => f.NIM == HttpContext.Session.GetString("NIM"));
                    if (log != null)
                    {
                        var date = DateTime.Parse(log.Tanggal.ToString()).Date;
                        if (date == tanggal)
                        {
                            ViewBag.Status = "Sudah";
                        }
                        else
                        {
                            ViewBag.Status = "Belum";
                        }
                    }
                    else
                    {
                        ViewBag.Status = "Belum";
                    }

                    
                }
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
