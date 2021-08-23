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
        private readonly IService<Ihaleler> _ihale;
        private readonly IService<Olumler> _olumler;
        private readonly IService<Videolar> _videolar;
        private readonly IService<Etkinlikler> _etkinlikler;
        private readonly IService<Personel> _belediyeBaskani;
        public HomeController(ILogger<HomeController> logger, IService<Haberler> haber, IService<Duyuru> duyuru, IService<Ihaleler> ihale, IService<Olumler> olumler, IService<Videolar> videolar, IService<Etkinlikler> etkinlikler,IService<Personel> belediyeBaskani)
        {
            _duyuru = duyuru;
            _haber = haber;
            _ihale = ihale;
            _olumler = olumler;
            _videolar = videolar;
            _etkinlikler = etkinlikler;
            _belediyeBaskani = belediyeBaskani;
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
            List<Ihaleler> ihaleListesi = _ihale.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Onay.Equals((byte)Enums.HaberDurumu.Onaylanmis));
            duyuruListesi.Reverse();
            ViewData["Ihaleler"] = ihaleListesi;
            List<Olumler> taziyeListesi = _olumler.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Onay.Equals((byte)Enums.HaberDurumu.Onaylanmis));
            duyuruListesi.Reverse();
            ViewData["Taziyeler"] = taziyeListesi;
            List<Videolar> videoListesi = _videolar.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif));
            duyuruListesi.Reverse();
            ViewData["Videolar"] = videoListesi;
            List<Etkinlikler> etkinlikListesi = _etkinlikler.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif));
            duyuruListesi.Reverse();
            ViewData["Etkinlikler"] = etkinlikListesi; 
            Personel belediyeBaskani = _belediyeBaskani.Find(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Statu.Equals((byte)Enums.Statu.BelediyeBaskani));
            duyuruListesi.Reverse();
            ViewData["BelediyeBaskani"] = belediyeBaskani;

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
