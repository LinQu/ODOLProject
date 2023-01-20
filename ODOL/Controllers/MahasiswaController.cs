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
                using (var reader = cmd.ExecuteReader())
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
        public JsonResult GetAllMahasiswa()
        {
            var mahasiswa = Get().ToList();
            return Json(new { data = mahasiswa });
        }


        public JsonResult GetMahasiswaByPeru(int id)
        {
            {
                var mahasiswa = (from mhs in _db.Mahasiswa
                                 join peru in _db.Perusahaan on mhs.IdPerusahaan equals peru.Id
                                 join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                 where mhs.IdPerusahaan == id && mhs.Status == "Aktif"
                                 select new ViewMhs
                                 {

                                     NIM = mhs.NIM,
                                     NamaMahasiswa = mhs.NamaMahasiswa,
                                     Prodi = mhs.Prodi,
                                     IdPerusahaan = mhs.IdPerusahaan,
                                     NamaPerusahaan = peng.Nama,
                                     Status = mhs.Status,
                                     CreateBy = mhs.CreateBy,
                                     CreateDate = mhs.CreateDate,
                                     ModifBy = mhs.ModifBy,
                                     ModifDate = mhs.ModifDate
                                 }).ToList();
                return Json(new { data = mahasiswa });
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
        public IActionResult Tambah([FromForm] Mahasiswa mahasiswa)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if(mahasiswa.IdPerusahaan == null)
                        {
                            TempData["Notifikasi"] = "Perusahan masih kosong";
                            TempData["Icon"] = "error";
                            return View(mahasiswa);
                        }
                        //check if nim already exist
                        var check = _db.Mahasiswa.Where(f => f.NIM == mahasiswa.NIM).FirstOrDefault();
                        if (check == null)
                        {
                            mahasiswa.Status = "Aktif";
                            mahasiswa.CreateBy = HttpContext.Session.GetInt32("Id");
                            mahasiswa.CreateDate = DateTime.Now;
                            _db.Mahasiswa.Add(mahasiswa);
                            _db.SaveChanges();
                            TempData["Notifikasi"] = "Data Berhasil Ditambahkan";
                            TempData["Icon"] = "success";
                            return RedirectToAction("Index");
                        }
                        else

                        {
                            TempData["Notifikasi"] = "Mahasiswa sudah ada terdaftar";
                            TempData["Icon"] = "error";
                            return View();
                        }

                    }
                    catch (Exception ex)
                    {
                        TempData["Notifikasi"] = "Data Gagal Ditambahkan" + ex.Message + mahasiswa.NamaMahasiswa;
                        TempData["Icon"] = "error";
                        ModelState.AddModelError("" ,"Eror karena : "+ ex.Message);
                        return View();
                    }
                }
                return View(mahasiswa);

            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Detail(string? nim)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                var mahasiswa = _db.Mahasiswa.Where(f => f.NIM == nim).FirstOrDefault();
                if (mahasiswa == null)
                {
                    TempData["Notifikasi"] = "Data tidak ditemukan";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Index", "Mahasiswa");
                }
                return View(mahasiswa);
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
