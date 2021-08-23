using App.Common.Filters;
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
    public class ProjelerController : Controller
    {
        private readonly IService<Projeler> _service;
        public ProjelerController(IService<Projeler> service)
        {
            _service = service;
        }
        public IActionResult ProjeSayfasi()
        {
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif));
            ViewBag.TamamlananProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi)).Count;
            ViewBag.ProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif)).Count;
            ViewBag.TamamlananIstihdamProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Istihdam)).Count;
            ViewBag.IstihdamProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Istihdam)).Count;
            ViewBag.TamamlananEgitimProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Egitim)).Count;
            ViewBag.EgitimProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Egitim)).Count;
            ViewBag.TamamlananSehircilikProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Sehircilik)).Count;
            ViewBag.SehircilikProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Sehircilik)).Count;
            ViewBag.TamamlananSosyalProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Sosyal)).Count;
            ViewBag.SosyalProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Sosyal)).Count;
            ViewBag.TamamlanaTurizmProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Turizm)).Count;
            ViewBag.TurizmProjeSayisi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.ProjeTuru.Equals((byte)Enums.ProjeTuru.Turizm)).Count;
            return View(entity);
        }
        public IActionResult FaaliyetRaporu()
        {
            return View();

        }

        [AutFilter]
        [HttpGet]
        public IActionResult Index()
        {
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif)); ;
            return View(entity);

        }
        [AutFilter]
        [HttpGet]
        public IActionResult YeniProjeler()
        {
            ViewBag.Baslik = "Yeni Proje";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public IActionResult YeniProjeler(Projeler entity)
        {
            entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.DegistirmeTarihi = DateTime.Now;
            entity.KaydedenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.KayıtTarihi = DateTime.Now;
            entity.Durum = (byte)Enums.KayitDurumu.Aktif;
            _service.Save(entity);
            return RedirectToAction("Index", "Projeler");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult ProjelerGuncelle(int? id)
        {
            ViewBag.Baslik = "Proje Düzenle";
            if (id == null)
            {
                return NotFound();
            }
            var entity = _service.GetById(id.Value);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }
        [AutFilter]
        [HttpPost]
        public IActionResult ProjelerGuncelle(Projeler entity)
        {
            ViewBag.Baslik = "Projeler Güncelle";
            if (ModelState.IsValid)
            {
                entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
                entity.DegistirmeTarihi = DateTime.Now;
                _service.Update(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }
        [AutFilter]
        [HttpGet]
        public IActionResult ProjelerDetaylar(int? id)
        {
            ViewBag.Baslik = "Projeler Detayları";
            if (id == null)
            {
                return NotFound();
            }
            var entity = _service.GetById(id.Value);
            if (entity == null)
            {
                return NotFound();//Bulunamadı
            }
            return View(entity);
        }
        public JsonResult ProjelerSil(int pId)
        {
            try
            {
                var entity = _service.GetById(pId);
                if (entity == null)
                {
                    return Json(new { basarili = false, id = pId, mesaj = "İşlem Başarısız" });
                }
                entity.Durum = (byte)Enums.KayitDurumu.Silinmiş;
                _service.Update(entity);
            }
            catch (Exception)
            {
                return Json(new { basarili = false, id = pId, mesaj = "İşlem Başarısız" });
            }
            return Json(new { basarili = true, id = pId, mesaj = "İşlem Başarılı" });
        }
    }
}
