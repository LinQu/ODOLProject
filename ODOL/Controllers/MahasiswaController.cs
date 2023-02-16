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
    public class MahasiswaController : Controller
    {
        private readonly ApplicationDBContext _db;

        SqlConnection con = new SqlConnection("Server=localhost;Database=DB_Odol;User Id=sa;Password=polman;TrustServerCertificate=True;");

        public List<RataNilai> getRata(string nim)
        {
            var penilaian = _db.Penilaian.Where(f => f.NIM == nim).ToList();

            // Membuat list untuk menyimpan hasil rata-rata nilai setiap baris
            List<RataNilai> rataNilai = new List<RataNilai>();

            // Looping setiap baris data nilai
            foreach (var item in penilaian)
            {
                // Menghitung rata-rata nilai setiap baris
                var rata = (item.PengetahuanKerja + item.KualitasKertja + item.KecepatanKerja + item.SikapPerilaku + item.KreatifitasKerjaSama + item.Leadership + item.Beradaptasi + item.PenangananMasalah) / 8;

                // Menambahkan hasil rata-rata nilai ke list
                rataNilai.Add(new RataNilai
                {
                    idPenilaian = item.idPenilaian,
                    NIM = item.NIM,
                    Periode = item.Periode,
                    Rata = rata
                });
            }

            // Mengembalikan list hasil rata-rata nilai
            return rataNilai;
        }

        public JsonResult test(string? nim)
        {
            var rata = getRata(nim);
            return Json(rata);


        }

        public async Task<JsonResult> SearchMhsApi(string? nim)
        {
            using (var client = new HttpClient())
            {
                // Menambahkan header untuk mengirim request dengan grant type password
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://api.polytechnic.astra.ac.id:2906/api_dev//AccessToken/Get"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "user"),
                    new KeyValuePair<string, string>("password", "pass")
                })
                };

                // Mengirim request ke API untuk mendapatkan token
                var response = await client.SendAsync(request);

                // Memeriksa apakah response sukses atau tidak
                if (!response.IsSuccessStatusCode)
                {

                    return null;
                }

                var token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());

                // Menambahkan header authorization dengan bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

                // Mengirim request untuk mendapatkan data dari API
                response = await client.GetAsync("https://api.polytechnic.astra.ac.id:2906/api_dev/efcc359990d14328fda74beb65088ef9660ca17e/SIA/getMahasiswa?nim=" + nim);

                // Memeriksa apakah response sukses atau tidak
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var data1 = JsonConvert.DeserializeObject<List<DaftarMhs>>(await response.Content.ReadAsStringAsync());


                return Json(new { data = data1 });
            }
        }

        public async Task<List<DaftarKonsen>> GetAllKonsen()
        {
            using (var client = new HttpClient())
            {
                // Menambahkan header untuk mengirim request dengan grant type password
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://api.polytechnic.astra.ac.id:2906/api_dev//AccessToken/Get"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "user"),
                    new KeyValuePair<string, string>("password", "pass")
                })
                };

                // Mengirim request ke API untuk mendapatkan token
                var response = await client.SendAsync(request);

                // Memeriksa apakah response sukses atau tidak
                if (!response.IsSuccessStatusCode)
                {

                    return null;
                }

                var token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());

                // Menambahkan header authorization dengan bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                // Mengirim request untuk mendapatkan data dari API
                response = await client.GetAsync("https://api.polytechnic.astra.ac.id:2906/api_dev/efcc359990d14328fda74beb65088ef9660ca17e/SIA/getListKonsentrasi");
                // Memeriksa apakah response sukses atau tidak
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var data = JsonConvert.DeserializeObject<List<DaftarKonsen>>(await response.Content.ReadAsStringAsync());
                //data = JsonConvert.DeserializeObject<List<DaftarMhs>>(await response.Content.ReadAsStringAsync());

                return data;
            }
        }
        public async Task<List<DaftarMhs>> DaftarMhs(int id)
        {
            using (var client = new HttpClient())
            {
                // Menambahkan header untuk mengirim request dengan grant type password
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://api.polytechnic.astra.ac.id:2906/api_dev//AccessToken/Get"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "user"),
                    new KeyValuePair<string, string>("password", "pass")
                })
                };

                // Mengirim request ke API untuk mendapatkan token
                var response = await client.SendAsync(request);

                // Memeriksa apakah response sukses atau tidak
                if (!response.IsSuccessStatusCode)
                {

                    return null;
                }

                var token = JsonConvert.DeserializeObject<TokenModel>(await response.Content.ReadAsStringAsync());

                // Menambahkan header authorization dengan bearer token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                // Mengirim request untuk mendapatkan data dari API
                response = await client.GetAsync("https://api.polytechnic.astra.ac.id:2906/api_dev/efcc359990d14328fda74beb65088ef9660ca17e/SIA/getListMahasiswa?id_konsentrasi=" + id);
                // Memeriksa apakah response sukses atau tidak
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var data = JsonConvert.DeserializeObject<List<DaftarMhs>>(await response.Content.ReadAsStringAsync());
                //data = JsonConvert.DeserializeObject<List<DaftarMhs>>(await response.Content.ReadAsStringAsync());

                return data;
            }
        }


        public MahasiswaController(ApplicationDBContext db)
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

        [HttpGet]
        public JsonResult GetAllMahasiswa1()
        {
            int i = 0;
            var mahasiswas = DaftarMhs(12).Result;
            var konsen = GetAllKonsen().Result;
            foreach (var kos in konsen)
            {
                var mahasiswa1 = DaftarMhs(kos.kon_id).Result;
                foreach (var item in mahasiswa1)
                {
                    mahasiswas.Add(item);
                }


            }

            return Json(new { data = mahasiswas });
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
                int idpemb = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                if (ViewBag.Role == "Mahasiswa")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (ViewBag.Role == "Pembimbing")
                {
                    var pemb = _db.Pembimbing.Where(f => f.idPengguna == idpemb).FirstOrDefault();
                    return View(_db.Mahasiswa.Where(f => f.IdPerusahaan == pemb.idPerusahaan));

                }
                else if(ViewBag.Role == "Perusahaan"){
                    var peru = _db.Perusahaan.Where(f => f.idPengguna == idpemb).FirstOrDefault();
                    if (peru == null)
                    {
                        TempData["Notifikasi"] = "Perusahaan Belum Terdaftar";
                        TempData["Icon"] = "error";
                        return RedirectToAction("Index", "Dashboard");
                    }
                    return View(_db.Mahasiswa.Where(f => f.IdPerusahaan == peru.Id));
                }
                else if (ViewBag.Role == "Prodi")
                {
                    return View(_db.Mahasiswa.Where(f =>  f.Prodi == HttpContext.Session.GetString("Nama")));
                }
                return View(_db.Mahasiswa);
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
                var mahasiswas = DaftarMhs(12).Result;
                var konsen = GetAllKonsen().Result;
                foreach (var kos in konsen)
                {
                    var mahasiswa1 = DaftarMhs(kos.kon_id).Result;
                    foreach (var item in mahasiswa1)
                    {
                        mahasiswas.Add(item);
                    }


                }
                ViewBag.DaftarMhs = mahasiswas;
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
                ViewBag.Nama = HttpContext.Session.GetString("Nama");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                var mahasiswas = DaftarMhs(12).Result;
                var konsen = GetAllKonsen().Result;
                foreach (var kos in konsen)
                {
                    var mahasiswa1 = DaftarMhs(kos.kon_id).Result;
                    foreach (var item in mahasiswa1)
                    {
                        mahasiswas.Add(item);
                    }


                }
                ViewBag.DaftarMhs = mahasiswas;
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (mahasiswa.IdPerusahaan == null)
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
                            mahasiswa.CreateBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
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
                        ModelState.AddModelError("", "Eror karena : " + ex.Message);
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

        public IActionResult Detail(string? NIM)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                //mengecek apakah nim sudah terdaftar atau belum

                var mahasiswa = _db.Mahasiswa.Where(f => f.NIM == NIM).FirstOrDefault();
                if (mahasiswa == null)
                {
                    TempData["Notifikasi"] = "Mahasiswa Belum Terdaftar";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Index", "Dashboard");
                }
                if (NIM != null)
                {
                    var mhs = _db.Mahasiswa.Where(f => f.NIM == NIM).FirstOrDefault();
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    ViewBag.NIM = HttpContext.Session.GetString("NIM");
                    ViewBag.Prodi = HttpContext.Session.GetString("Prodi");
                    ViewBag.LogBook = GetLogBookBy(NIM);
                    ViewBag.Rata = getRata(NIM);
                    ViewBag.Perusahaan = (from peru in _db.Perusahaan
                                          join peng in _db.Pengguna on peru.idPengguna equals peng.Id
                                          where peru.Status == "Aktif" && peru.Id == mhs.IdPerusahaan
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
                    return View("/Views/Profile/Index.cshtml", _db.Mahasiswa.Where(f => f.NIM == NIM).FirstOrDefault());
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

        public IActionResult Ubah(string? NIM)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                //mengecek apakah nim sudah terdaftar atau belum

                var mahasiswa = _db.Mahasiswa.Where(f => f.NIM == NIM).FirstOrDefault();
                if (mahasiswa == null)
                {
                    TempData["Notifikasi"] = "Mahasiswa Belum Terdaftar";
                    TempData["Icon"] = "error";
                    return RedirectToAction("Index", "Dashboard");
                }
                if (NIM != null)
                {
                    ViewBag.Nama = HttpContext.Session.GetString("Nama");
                    ViewBag.Role = HttpContext.Session.GetString("Role");
                    ViewBag.NIM = HttpContext.Session.GetString("NIM");
                    ViewBag.Prodi = HttpContext.Session.GetString("Prodi");
                    ViewBag.Perusahaan = (from peru in _db.Perusahaan
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
                    var mhs = _db.Mahasiswa.Where(f => f.NIM == NIM).FirstOrDefault();
                    ViewBag.Peru = _db.Perusahaan.Where(f => f.Id == mhs.IdPerusahaan ).FirstOrDefault();
                    return View("Ubah", mhs );
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

        [HttpPost]

        public async Task<IActionResult> Ubah(Mahasiswa mahasiswa)
        {
            if (HttpContext.Session.GetString("Nama") != null)
            {
                mahasiswa.ModifBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                mahasiswa.ModifDate = DateTime.Now;
                _db.Mahasiswa.Update(mahasiswa);
                await _db.SaveChangesAsync();
                TempData["Notifikasi"] = "Data Berhasil Diubah";
                TempData["Icon"] = "success";
                return RedirectToAction("Detail", "Mahasiswa", new { NIM = mahasiswa.NIM });


            }
            else
            {
                TempData["Notifikasi"] = "Anda Harus Login Terlebih Dahulu";
                TempData["Icon"] = "error";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string? status, string? nim)
        {
            if (HttpContext.Session.GetString("Nama") != null && HttpContext.Session.GetString("Role") == "Admin")
            {
                try
                {
                    var mhs = _db.Mahasiswa.Find(nim);
                    mhs.Status = status;

                    _db.Mahasiswa.Update(mhs);
                    await _db.SaveChangesAsync();
                    TempData["Notifikasi"] = "Status Berhasil Diubah";
                    TempData["Icon"] = "success";
                    return RedirectToAction("Detail", "Mahasiswa", new { NIM = nim });

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    TempData["Notifikasi"] = "Status Gagal Diubah";
                    TempData["icon"] = "error";
                    return RedirectToAction("Detail", "Mahasiswa", new { NIM = nim });
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

    public class DaftarKonsen
    {
        public int kon_id { get; set; }
        public string kon_nama { get; set; }
    }
}
