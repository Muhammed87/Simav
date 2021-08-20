using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simav.Common;
using Simav.Core;
using Simav.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Simav.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Haberler> _haber;
        private readonly IService<Duyuru> _duyuru;
        public HomeController(ILogger<HomeController> logger,IService<Haberler> haber, IService<Duyuru> duyuru)
        {
            _duyuru = duyuru;
            _haber = haber;
            _logger = logger;
        }

        public IActionResult Index()
        {

            List<Haberler> haberListesi = _haber.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Onay.Equals((byte)Enums.HaberDurumu.Onaylanmis));
            haberListesi.Reverse();
            ViewData["Haberler"] = haberListesi; 
            List<Duyuru> duyuruListesi = _duyuru.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Onay.Equals((byte)Enums.HaberDurumu.Onaylanmis));
            duyuruListesi.Reverse();
            ViewData["Duyurular"] = duyuruListesi;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
