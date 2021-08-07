using App.Common.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simav.Common;
using Simav.Core;
using Simav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simav.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IService<Kullanicilar> _service;
        public KullaniciController(IService<Kullanicilar> service)
        {
            _service = service;
        }
        [AutFilter]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Baslik = "Kullanici Listesi";
            var kullaniciListesi = _service.GetAll();
            return View(kullaniciListesi);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Kullanicilar entity)
        {
            var kullanici = _service.Find(x => x.KullaniciAdi.Equals(entity.KullaniciAdi) && x.Password.Equals(entity.Password));
            if (kullanici != null)
            {
                HttpContext.Session.SetString("OturumAcik", "true");
                SessionInfo.GirisTarihi = DateTime.Now;
                SessionInfo.GirisYapanKullaniciId = kullanici.KullaniciId;
                SessionInfo.KullaniciAdSoyad = kullanici.Ad + " " + kullanici.Soyad;
                return RedirectToAction("Index", "Haberler");
            }
            else
            {
                ModelState.AddModelError("", "Kullanici veya Şifre Hatalı!");
                return View();
            }
        }
    }
}
