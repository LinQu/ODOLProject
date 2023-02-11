using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;
using Microsoft.Data.SqlClient;

namespace ODOL.Controllers
{
    public class LogBookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public LogBookController(ApplicationDBContext db)
        {
            _db = db;
        }

        public JsonResult GetDetailLog(string? NIM, int? id)
        {
            var logbook = (from log in _db.LogBook
                           join mhs in _db.Mahasiswa on log.NIM equals mhs.NIM
                           join peru in _db.Perusahaan on mhs.IdPerusahaan equals peru.Id
                           join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                           where log.NIM == NIM && log.idLogBook == id
                           select new ViewLogBook
                           {
                               id = log.idLogBook,
                               NIM = log.NIM,
                               idPembimbing = log.idPembimbing,
                               NamaMahasiswa = mhs.NamaMahasiswa,
                               NamaPerusahaan = peng.Nama,
                               Cabang = peru.Cabang,
                               Group = peru.Group,
                               Departemen = log.Departemen,
                               Kegiatan = log.Kegiatan,
                               RincianKegiatan = log.RincianKegiatan,
                               JamStart = log.JamStart,
                               JamEnd = log.JamEnd,
                               Tanggal = log.Tanggal,
                               Ulasan = log.Ulasan,
                               Status = log.Status,
                               CreatedBy = log.CreatedBy,
                               CreatedDate = log.CreatedDate
                           }).FirstOrDefault();
            return Json(new { data = logbook });

        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string? ulasan,string status)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Pembimbing")
                {
                    var search = _db.LogBook.Where(f => f.idLogBook == id).FirstOrDefault();
                    search.Ulasan = ulasan;
                    search.Status = status;
                    LogBook log = search;
                    _db.LogBook.Update(log);
                    _db.SaveChanges();
                    return RedirectToAction("Daftar", "LogBook");
                }else
                {
                    TempData["Notifikasi"] = "Anda Tidak Punya Akses";
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

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (HttpContext.Session.GetString("Role") == "MAHASISWA")
                {
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    ViewBag.NIM = HttpContext.Session.GetString("NIM");
                    var NIM = HttpContext.Session.GetString("NIM");
                    ViewBag.Status = CheckLog();
                    ViewBag.Tableid = "myTableLogBook" + CheckLog();
                    return View(_db.LogBook.Where(f => f.NIM == NIM).ToList());
                }
                else
                {
                    TempData["Notifikasi"] = "Anda Bukan Mahasiswa";
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

        public string CheckLog()
        {
            //mengecek apakah harini sudah mengisi logbook
            var tanggal = DateTime.Now.Date;
            var log = _db.LogBook.OrderBy(f => f.Tanggal).LastOrDefault(f => f.NIM == HttpContext.Session.GetString("NIM"));
            if (log != null)
            {
                var date = DateTime.Parse(log.Tanggal.ToString()).Date;
                if (date == tanggal)
                {
                    return "Sudah";
                }
                else
                {
                    return "Belum";
                }
            }
            else
            {
                return "Belum";
            }
        }
    

        public IActionResult Daftar()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (HttpContext.Session.GetString("Role") == "Pembimbing")
                {
                    var idPeng = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    var idPemb = _db.Pembimbing.Where(f => f.idPengguna == idPeng).FirstOrDefault();
                    var logbook = _db.LogBook.Where(f => f.idPembimbing == idPemb.id && f.Status == "Belum Disetujui").ToList();
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    ViewBag.Id = HttpContext.Session.GetString("Id");
                    return View(logbook);
                }
                else
                {
                    TempData["Notifikasi"] = "Anda Tidak Punya Akses";
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


        public IActionResult Tambah()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (HttpContext.Session.GetString("Role") == "MAHASISWA")
                {
                    //jika HttpContext.Session.GetString("NIM") tidak terdaftar di database mahasiswa
                    var val = _db.Mahasiswa.Where(f => f.NIM == HttpContext.Session.GetString("NIM")).FirstOrDefault();
                    if (val != null)
                    {

                        var mahasiswa = (from mhs in _db.Mahasiswa
                                         join peru in _db.Perusahaan on mhs.IdPerusahaan equals peru.Id
                                         join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                         where mhs.NIM == HttpContext.Session.GetString("NIM") && mhs.Status == "Aktif"
                                         select new ViewMhs
                                         {

                                             NIM = mhs.NIM,
                                             NamaMahasiswa = mhs.NamaMahasiswa,
                                             Prodi = mhs.Prodi,
                                             IdPerusahaan = mhs.IdPerusahaan,
                                             NamaPerusahaan = peng.Nama,
                                             Cabang = peru.Cabang,
                                             Group = peru.Group,
                                             Status = mhs.Status,
                                             CreateBy = mhs.CreateBy,
                                             CreateDate = mhs.CreateDate,
                                             ModifBy = mhs.ModifBy,
                                             ModifDate = mhs.ModifDate
                                         }).FirstOrDefault();
                        var perusahaan = (from peru in _db.Perusahaan
                                          join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                          where peru.Status == "Aktif" && peru.Id == mahasiswa.IdPerusahaan
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
                                              CreateBy = peru.CreateBy,
                                              CreateDate = peru.CreateDate,
                                              ModifBy = peru.ModifBy,
                                              ModifDate = peru.ModifDate
                                          }).FirstOrDefault();
                        ViewBag.Nama = HttpContext.Session.GetString("Nama");
                        ViewBag.Role = HttpContext.Session.GetString("Role");
                        ViewBag.NIM = HttpContext.Session.GetString("NIM");
                        ViewBag.Prodi = HttpContext.Session.GetString("Prodi");
                        ViewBag.Perusahaan = mahasiswa.NamaPerusahaan;
                        ViewBag.Cabang = mahasiswa.Cabang;
                        ViewBag.Group = mahasiswa.Group;
                        ViewBag.Pembimbing = perusahaan.DaftarPembimbing;
                        return View();
                    }
                    else
                    {
                        TempData["Notifikasi"] = "Anda Tidak Terdaftar atau Tidak Aktif";
                        TempData["Icon"] = "error";
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    TempData["Notifikasi"] = "Anda Bukan Mahasiswa";
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

        [HttpPost]
        public async Task<IActionResult> Tambah(LogBook logBook)
        {

            if (HttpContext.Session.GetString("Nama") != null)
            {

                if (HttpContext.Session.GetString("Role") == "MAHASISWA")
                {
                    try
                    {
                        logBook.NIM = HttpContext.Session.GetString("NIM");
                        if (ModelState.IsValid)
                        {

                            logBook.CreatedBy = HttpContext.Session.GetString("NIM");
                            logBook.Status = "Belum Disetujui";
                            _db.LogBook.Add(logBook);
                            await _db.SaveChangesAsync();
                            TempData["Notifikasi"] = "Data Berhasil Ditambahkan";
                            TempData["Icon"] = "success";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Notifikasi"] = "Data Gagal Ditambahkan";
                            TempData["Icon"] = "error";
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception e1)
                    {
                        ModelState.AddModelError("Error DI :", e1.Message);
                        return View(logBook);
                    }
                }
                else
                {
                    TempData["Notifikasi"] = "Anda Bukan Mahasiswa";
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

        public IActionResult Detail(string? NIM, int? Id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (HttpContext.Session.GetString("Role") == "MAHASISWA" || HttpContext.Session.GetString("Role") == "Pembimbing" || HttpContext.Session.GetString("Role") == "Admin" || HttpContext.Session.GetString("Role") == "Prodi")
                {
                    var logbook = (from log in _db.LogBook
                                   join mhs in _db.Mahasiswa on log.NIM equals mhs.NIM
                                   join peru in _db.Perusahaan on mhs.IdPerusahaan equals peru.Id
                                   join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                   where log.NIM == NIM && log.idLogBook == Id
                                   select new ViewLogBook
                                   {
                                       id = log.idLogBook,
                                       NIM = log.NIM,
                                       idPembimbing = log.idPembimbing,
                                       NamaMahasiswa = mhs.NamaMahasiswa,
                                       Prodi = mhs.Prodi,
                                       NamaPerusahaan = peng.Nama,
                                       Cabang = peru.Cabang,
                                       Group = peru.Group,
                                       Departemen = log.Departemen,
                                       Kegiatan = log.Kegiatan,
                                       RincianKegiatan = log.RincianKegiatan,
                                       JamStart = log.JamStart,
                                       JamEnd = log.JamEnd,
                                       Tanggal = log.Tanggal,
                                       Ulasan = log.Ulasan,
                                       Status = log.Status,
                                       CreatedBy = log.CreatedBy,
                                       CreatedDate = log.CreatedDate
                                   }).FirstOrDefault();
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    ViewBag.NIM = logbook.NIM;
                    ViewBag.Prodi = logbook.Prodi;
                    ViewBag.NamaMhs = logbook.NamaMahasiswa;
                    ViewBag.Perusahaan = logbook.NamaPerusahaan;
                    ViewBag.Cabang = logbook.Cabang;
                    ViewBag.Group = logbook.Group;
                    var log1 = _db.LogBook.Where(f => f.NIM == NIM && f.idLogBook == Id).FirstOrDefault();
                    return View(log1);
                }
                else
                {
                    TempData["Notifikasi"] = "Anda Bukan Mahasiswa";
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
