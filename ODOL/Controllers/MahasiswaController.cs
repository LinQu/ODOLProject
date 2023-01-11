using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;
using Microsoft.Data.SqlClient;

namespace ODOL.Controllers
{
    public class MahasiswaController : Controller
    {
        private readonly ApplicationDBContext _db;

        SqlConnection con = new SqlConnection("Server=localhost;Database=DB_Odol;User Id=sa;Password=polman;TrustServerCertificate=True;");



        public MahasiswaController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IEnumerable<TempMhs> Get()
        {
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT * FROM tempMahasiswa";
                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new TempMhs
                        {
                            NIM = reader.GetString(0),
                            Nama = reader.GetString(1),
                            Prodi = reader.GetString(2),
                        };
                    }
                }
            }
        }

        [HttpGet]
        public JsonResult SearchMhs(string search)
        {
            var data = Get().Where(x => x.NIM.Contains(search) || x.Nama.Contains(search) || x.Prodi.Contains(search)).ToList();


            return Json(data);
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View(_db.Mahasiswa.Where(f => f.Status == "Aktif"));
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
                var Temp = Get().ToList();
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return View(Temp);
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
