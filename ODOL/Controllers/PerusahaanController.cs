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

            var perusahaan = (from peru in _db.Perusahaan
                              join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                              where peru.Status == "Aktif"
                              select new ViewPeru
                              {
                                  Id = peru.Id,
                                  idPengguna = peru.idPengguna,
                                  NamaPerusahaan = peng.Nama,
                                  AlamatPerusahaan = peru.AlamatPerusahaan,
                                  EmailPerusahaan = peru.EmailPerusahaan,
                                  Cabang = peru.Cabang,
                                  Group = peru.Group,
                                  Status = peru.Status,
                                  DaftarPembimbing = (from pem in _db.Pembimbing
                                                      join peng in _db.Pengguna on pem.idPengguna equals peng.Id
                                                      where pem.Status == "Aktif" && pem.idPerusahaan == peru.Id
                                                      select new ViewPem
                                                      {
                                                          id = pem.id,
                                                          idPengguna = pem.idPengguna,
                                                          idPerusahaan = pem.idPerusahaan,
                                                          NamaPembimbing = peng.Nama,
                                                          EmailPembimbing = pem.EmailPembimbing,
                                                          Jabatan = pem.Jabatan,
                                                          Status = pem.Status,
                                                          CreateBy = pem.CreateBy,
                                                          CreateDate = pem.CreateDate,
                                                          ModifBy = pem.ModifBy,
                                                          ModifDate = pem.ModifDate
                                                      }).ToList(),
                                  DaftarMahasiswa = _db.Mahasiswa.Where(f => f.Status == "Aktif" && f.IdPerusahaan == peru.Id).ToList(),
                                  CreateBy = (int)peru.CreateBy,
                                  CreateDate = peru.CreateDate,
                                  ModifBy = (int)peru.ModifBy,
                                  ModifDate = peru.ModifDate
                              }).ToList();
            return Json(new { data = perusahaan });

        }

        [HttpGet]
        public JsonResult SearchPeru(int search)
        {
            var perusahaan = (from peru in _db.Perusahaan
                              join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                              where peru.Status == "Aktif" && peru.Id == search
                              select new ViewPeru
                              {
                                  Id = peru.Id,
                                  idPengguna = peru.idPengguna,
                                  NamaPerusahaan = peng.Nama,
                                  AlamatPerusahaan = peru.AlamatPerusahaan,
                                  EmailPerusahaan = peru.EmailPerusahaan,
                                  Cabang = peru.Cabang,
                                  Group = peru.Group,
                                  Status = peru.Status,
                                  CreateBy = (int)peru.CreateBy,
                                  CreateDate = peru.CreateDate,
                                  ModifBy = (int)peru.ModifBy,
                                  ModifDate = peru.ModifDate
                              }).ToList();
            return Json(new { data = perusahaan });
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                //menampilkan nama dari data pengguna taruh ke model ViewPeru
                var perusahaan = (from peru in _db.Perusahaan
                                  join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                  where peru.Status == "Aktif"
                                  select new ViewPeru
                                  {
                                      Id = peru.Id,
                                      idPengguna = peru.idPengguna,
                                      NamaPerusahaan = peng.Nama,
                                      AlamatPerusahaan = peru.AlamatPerusahaan,
                                      EmailPerusahaan = peru.EmailPerusahaan,
                                      Cabang = peru.Cabang,
                                      Group = peru.Group,
                                      Status = peru.Status,
                                      DaftarPembimbing = (from pem in _db.Pembimbing
                                                          join peng in _db.Pengguna on pem.idPengguna equals peng.Id
                                                          where pem.Status == "Aktif" && pem.idPerusahaan == peru.Id
                                                          select new ViewPem
                                                          {
                                                              id = pem.id,
                                                              idPengguna = pem.idPengguna,
                                                              idPerusahaan = pem.idPerusahaan,
                                                              NamaPembimbing = peng.Nama,
                                                              EmailPembimbing = pem.EmailPembimbing,
                                                              Jabatan = pem.Jabatan,
                                                              Status = pem.Status,
                                                              CreateBy = pem.CreateBy,
                                                              CreateDate = pem.CreateDate,
                                                              ModifBy = pem.ModifBy,
                                                              ModifDate = pem.ModifDate
                                                          }).ToList(),
                                      DaftarMahasiswa = _db.Mahasiswa.Where(f => f.Status == "Aktif" && f.IdPerusahaan == peru.Id).ToList(),
                                      CreateBy = (int)peru.CreateBy,
                                      CreateDate = peru.CreateDate,
                                      ModifBy = (int)peru.ModifBy,
                                      ModifDate = peru.ModifDate
                                  }).ToList();

                return View(perusahaan);
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
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");

                if (ModelState.IsValid)
                {
                    perusahaan.Status = "Aktif";
                    perusahaan.CreateBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    perusahaan.CreateDate = DateTime.Now;

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
                var perusahaan = (from peru in _db.Perusahaan
                                  join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                  where peru.Status == "Aktif" && peru.Id == id
                                  select new ViewPeru
                                  {
                                      Id = peru.Id,
                                      idPengguna = peru.idPengguna,
                                      NamaPerusahaan = peng.Nama,
                                      AlamatPerusahaan = peru.AlamatPerusahaan,
                                      EmailPerusahaan = peru.EmailPerusahaan,
                                      Cabang = peru.Cabang,
                                      Group = peru.Group,
                                      Status = peru.Status,
                                      DaftarPembimbing = (from pem in _db.Pembimbing
                                                          join peng in _db.Pengguna on pem.idPengguna equals peng.Id
                                                          where pem.Status == "Aktif" && pem.idPerusahaan == peru.Id
                                                          select new ViewPem
                                                          {
                                                              id = pem.id,
                                                              idPengguna = pem.idPengguna,
                                                              idPerusahaan = pem.idPerusahaan,
                                                              NamaPembimbing = peng.Nama,
                                                              EmailPembimbing = pem.EmailPembimbing,
                                                              Jabatan = pem.Jabatan,
                                                              Status = pem.Status,
                                                              CreateBy = pem.CreateBy,
                                                              CreateDate = pem.CreateDate,
                                                              ModifBy = pem.ModifBy,
                                                              ModifDate = pem.ModifDate
                                                          }).ToList(),
                                      DaftarMahasiswa = _db.Mahasiswa.Where(f => f.Status == "Aktif" && f.IdPerusahaan == peru.Id).ToList(),
                                      CreateBy = (int)peru.CreateBy,
                                      CreateDate = peru.CreateDate,
                                      ModifBy = (int)peru.ModifBy,
                                      ModifDate = peru.ModifDate
                                  }).FirstOrDefault();
                return View(perusahaan);
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
                var perusahaan = (from peru in _db.Perusahaan
                                  join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                  where peru.Status == "Aktif" && peru.Id == id
                                  select new ViewPeru
                                  {
                                      Id = peru.Id,
                                      idPengguna = peru.idPengguna,
                                      NamaPerusahaan = peng.Nama,
                                      AlamatPerusahaan = peru.AlamatPerusahaan,
                                      EmailPerusahaan = peru.EmailPerusahaan,
                                      Cabang = peru.Cabang,
                                      Group = peru.Group,
                                      Status = peru.Status,
                                      CreateBy = (int)peru.CreateBy,
                                      CreateDate = peru.CreateDate,
                                      ModifBy = (int)peru.ModifBy,
                                      ModifDate = peru.ModifDate
                                  }).FirstOrDefault();
                return View(perusahaan);
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ubah([FromForm] Perusahaan perusahaan)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                if (ModelState.IsValid)
                {

                    perusahaan.ModifBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    perusahaan.ModifDate = DateTime.Now;
                    _db.Perusahaan.Update(perusahaan);
                    await _db.SaveChangesAsync();
                    TempData["Notifikasi"] = "Data Berhasil Diubah";
                    TempData["Icon"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Notifikasi"] = "Data Gagal Diubah";
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

        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                try
                {
                    var Perusahaan = _db.Perusahaan.Find(id);
                    Perusahaan.Status = "Tidak Aktif";

                    _db.Perusahaan.Update(Perusahaan);
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
