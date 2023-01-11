﻿using Microsoft.AspNetCore.Mvc;
using ODOL.Models;
using ODOL.Data;
using System.Diagnostics;

namespace ODOL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public object Session { get; private set; }

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Pengguna pengguna)
        {
            try
            {
                var status = "Aktif";
                    var login = _db.Pengguna.Where(x => x.Username.Equals(pengguna.Username) && x.Password.Equals(pengguna.Password) && x.Status.Equals(status)).FirstOrDefault();
                    if (login != null)
                    {
                    HttpContext.Session.SetString("Role", "Admin");
                    HttpContext.Session.SetString("Nama", login.Nama);
                    HttpContext.Session.SetString("Id", login.Id.ToString());
                    return RedirectToAction("Index", "Dashboard");

                    }
                else
                {
                    TempData["Notifikasi"] = "Username atau Password Salah";
                    TempData["Icon"] = "error";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["Notifikasi"] = ex.Message;

            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Nama");
            HttpContext.Session.Remove("Id");
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}