using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;

namespace ODOL.Controllers
{
    public class PembimbingController : Controller
    {
        private readonly ApplicationDBContext _db;



        public PembimbingController(ApplicationDBContext db)
        {
            _db = db;
        }

        public JsonResult GettPembimbingById(int id)
        {
            var pembimbing = (from pem in _db.Pembimbing
                              join peng in _db.Pengguna on pem.idPengguna equals peng.Id
                              where pem.Status == "Aktif" && pem.id == id
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
                              }).FirstOrDefault();

            return Json(new { data = pembimbing });
        }

        public JsonResult GetAllPembimbing(int id)
        {
            var pembimbing = (from pem in _db.Pembimbing
                              join peng in _db.Pengguna on pem.idPengguna equals peng.Id
                              where pem.Status == "Aktif" && pem.idPerusahaan == id
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
                              }).ToList();

            return Json(new { data = pembimbing });

        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                return RedirectToAction("Tambah", "Pembimbing");
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Tambah(int id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Peru = id;
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
        public async Task<IActionResult> Tambah([FromForm] ViewPem viewpem)
        {

            if (HttpContext.Session.GetString("Nama") != null)
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        var pembimbing = new Pembimbing();
                        pembimbing.idPengguna = viewpem.idPengguna;
                        pembimbing.idPerusahaan = viewpem.idPerusahaan;
                        pembimbing.Jabatan = viewpem.Jabatan;
                        pembimbing.EmailPembimbing = viewpem.EmailPembimbing;
                        pembimbing.Status = "Aktif";
                        pembimbing.CreateBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                        pembimbing.CreateDate = DateTime.Now;
                        _db.Pembimbing.Add(pembimbing);
                        await _db.SaveChangesAsync();
                        TempData["Notifikasi"] = "Data Berhasil Ditambahkan";
                        TempData["Icon"] = "success";
                        return RedirectToAction("Detail", "Perusahaan", new { id = viewpem.idPerusahaan });
                    }
                    catch (Exception e1)
                    {
                        ModelState.AddModelError("", e1.Message);
                        return RedirectToAction("Tambah", "Pembimbing", new { id = viewpem.idPerusahaan });
                    }
                }
                else
                {
                    TempData["Notifikasi"] = "Data Gagal Ditambahkan";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Tambah", "Pembimbing", new { id = viewpem.idPerusahaan });
                }

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
                try
                {
                    
                    var pembimbing = (from pem in _db.Pembimbing
                                      join peng in _db.Pengguna on pem.idPengguna equals peng.Id
                                      where pem.Status == "Aktif" && pem.id == id
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
                                      }).FirstOrDefault();
                    ViewBag.Peru = pembimbing.idPerusahaan;
                    return View(pembimbing);
                }
                catch (Exception e1)
                {
                    ModelState.AddModelError("", e1.Message);
                    return RedirectToAction("Detail", "Perusahaan", new { id = id });
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
        public async Task<IActionResult> Ubah([FromForm] ViewPem viewpem)
        {
            Pembimbing pembimbing = _db.Pembimbing.Find(viewpem.id);
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (ModelState.IsValid)
                {
                    pembimbing.idPerusahaan = viewpem.idPerusahaan;
                    pembimbing.Jabatan = viewpem.Jabatan;
                    pembimbing.EmailPembimbing = viewpem.EmailPembimbing;
                    pembimbing.ModifBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    pembimbing.ModifDate = DateTime.Now;
                    _db.Pembimbing.Update(pembimbing);
                    await _db.SaveChangesAsync();
                    TempData["Notifikasi"] = "Data Berhasil Diubah";
                    TempData["Icon"] = "success";
                    return RedirectToAction("Detail", "Perusahaan", new { id = pembimbing.idPerusahaan });

                }
                else
                {
                    TempData["Notifikasi"] = "Data Gagal Ditambahkan";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Ubah", "Pembimbing", new { id = viewpem.idPerusahaan });
                }

            }
            else
            {
                TempData["Notifikasi"] = "Data Gagal Ditambahkan";
                TempData["Icon"] = "error";
                return RedirectToAction("Tambah", "Pembimbing", new { id = viewpem.idPerusahaan });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            {
                if (HttpContext.Session.GetString("Nama") != null)
                {
                    try
                    {
                        var pembimbing = _db.Pembimbing.Find(id);
                        pembimbing.Status = "Tidak Aktif";
                        pembimbing.ModifBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                        pembimbing.ModifDate = DateTime.Now;
                        _db.Pembimbing.Update(pembimbing);
                        await _db.SaveChangesAsync();
                        TempData["Notifikasi"] = "Data Berhasil Dihapus";
                        TempData["Icon"] = "success";
                        return RedirectToAction("Detail", "Perusahaan", new { id = pembimbing.idPerusahaan });
                    }
                    catch (Exception e1)
                    {
                        ModelState.AddModelError("", e1.Message);
                        return RedirectToAction("Detail", "Perusahaan", new { id = id });
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
}
