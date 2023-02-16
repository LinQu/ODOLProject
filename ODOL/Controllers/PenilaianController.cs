using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Linq;


namespace ODOL.Controllers
{
    public class DataNilai
    {
        
        public string? NIM { get; set; }
        public string? Nama { get; set; }
        public string? Prodi { get; set; }
        public string? Periode { get; set; }
        public string? Pembimbing { get; set; }
        public Penilaian Nilai { get; set; }
        public RataNilai Rata { get; set; }
        
    }

    public class PenilaianController : Controller
    {
        private readonly ApplicationDBContext _db;

        public PenilaianController(ApplicationDBContext db)
        {
            _db = db;
        }

        public JsonResult GetNilai(int id)
        {
            var nilai = _db.Penilaian.Where(f => f.idPenilaian == id).FirstOrDefault();
            var mhs = _db.Mahasiswa.Where(f => f.NIM == nilai.NIM).FirstOrDefault();
            var pemb = _db.Pengguna.Where(f => f.Id == nilai.idPembimbing).FirstOrDefault();

            DataNilai data = new DataNilai{
                NIM = nilai.NIM,
                Nama = mhs.NamaMahasiswa,
                Prodi = mhs.Prodi,
                Periode = nilai.Periode,
                Pembimbing = pemb.Nama,
                Nilai = nilai,
                Rata = getRata(id).FirstOrDefault(),
            };
            return Json(data);
        }


        public List<RataNilai> getRata(int id)
        {
            //membuat rata rata seluruh nilai
            var rata = _db.Penilaian.Where(f => f.idPenilaian == id).ToList();

            var rata2 = rata.GroupBy(f => f.NIM).Select(f => new RataNilai

            {
                idPenilaian = f.Select(g => g.idPenilaian).FirstOrDefault(),
                NIM = f.Key,
                Periode = f.Select(g => g.Periode).FirstOrDefault(),
                Rata = f.Average(g => (g.PengetahuanKerja + g.KualitasKertja + g.KecepatanKerja + g.SikapPerilaku + g.KreatifitasKerjaSama + g.Leadership + g.Beradaptasi + g.PenangananMasalah)) / 8
            }).ToList();


            return rata2;
        }

        private List<Mahasiswa> GetMhs(int id)
        {
            {
                var peru = _db.Pembimbing.Where(f => f.idPengguna == id).FirstOrDefault();
                var mhs = _db.Mahasiswa.Where(f=> f.IdPerusahaan == peru.idPerusahaan).ToList();
                if (mhs.Count > 0)
                {
                    return mhs;
                }
                else
                {
                    return null;
                }
            }
        }
       
        public IActionResult Index()
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

        public IActionResult Tambah()
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                var id = Convert.ToInt32(HttpContext.Session.GetString("Id"));

                ViewBag.Mahasiswa = GetMhs(id);
                ViewBag.id = id;
                
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
        public IActionResult Tambah(Penilaian penilaian)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");

                //mengecek apakah periode tersebut sudah ada atau belum
                var cek = _db.Penilaian.Where(f => f.NIM == penilaian.NIM && f.Periode == penilaian.Periode).FirstOrDefault();
                if (cek == null)
                {
                    penilaian.CreateadBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    penilaian.CreatedDate = DateTime.Now;
                    _db.Penilaian.Add(penilaian);
                    _db.SaveChanges();
                    TempData["Notifikasi"] = "Data Berhasil Ditambahkan";
                    TempData["Icon"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Notifikasi"] = "Penilaian Bulan " + penilaian.Periode + " Sudah Ada";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult Detail(int? id)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                if (id != null)
                {
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    var nilai = _db.Penilaian.Where(f => f.idPenilaian == id).FirstOrDefault();
                    ViewBag.Penilaian = nilai;
                    ViewBag.Rata = getRata(nilai.idPenilaian).FirstOrDefault();
                    ViewBag.Mahasiswa = _db.Mahasiswa.Where(f => f.NIM == nilai.NIM).FirstOrDefault();
                    ViewBag.CreateBy = _db.Pengguna.Where(f => f.Id == nilai.CreateadBy).Select(f => f.Nama).FirstOrDefault();
                    return View();
                }
                else
                {
                    TempData["Notifikasi"] = "Data Tidak Ditemukan";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Index", "Penilaian");
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
