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
            var tamamlananProjeler= entity.Where(x => x.ProjeDurumu.Equals((byte)Enums.ProjeDurumu.Tamamlandi));
            float TamamlananProjeSayisi = tamamlananProjeler.Count();
            float ProjeSayisi= entity.Count();
            ViewBag.TamamlananProjeYuzdesi = (TamamlananProjeSayisi / ProjeSayisi)*100;
            float TamamlananIstihdamProjeSayisi = tamamlananProjeler.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Istihdam)).Count();
            float IstihdamProjeSayisi = entity.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Istihdam)).Count();
            ViewBag.TamamlananIstihdamProjeYuzdesi = (TamamlananIstihdamProjeSayisi / IstihdamProjeSayisi)*100;
            float TamamlananEgitimProjeSayisi = tamamlananProjeler.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Egitim)).Count();
            float EgitimProjeSayisi = entity.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Egitim)).Count();
            ViewBag.TamamlananEgitimProjeYuzdesi = (TamamlananEgitimProjeSayisi / EgitimProjeSayisi) * 100;
            float TamamlananTurizmProjeSayisi = tamamlananProjeler.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Turizm)).Count();
            float TurizmProjeSayisi= entity.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Turizm)).Count();
            ViewBag.TamamlananTurizmProjeYuzdesi = (TamamlananTurizmProjeSayisi / TurizmProjeSayisi) * 100;
            float TamamlananSosyalProjeSayisi = tamamlananProjeler.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Sosyal)).Count();
            float SosyalProjeSayisi= entity.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Sosyal)).Count();
            ViewBag.TamamlananSosyalProjeYuzdesi = (TamamlananSosyalProjeSayisi / SosyalProjeSayisi) * 100;
            float TamamlananSehircilikProjeSayisi = tamamlananProjeler.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Sehircilik)).Count();
            float SehircilikProjeSayisi = entity.Where(x => x.ProjeTuru.Equals((int)Enums.ProjeTuru.Sehircilik)).Count();
            ViewBag.TamamlananSehircilikProjeYuzdesi = (TamamlananSehircilikProjeSayisi / SehircilikProjeSayisi) * 100;
            
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
