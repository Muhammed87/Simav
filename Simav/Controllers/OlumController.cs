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
    public class OlumController : Controller
    {
        private readonly IService<Olumler> _service;
        public OlumController(IService<Olumler> service)
        {
            _service = service;
        }

        public IActionResult TaziyeListesi()
        {
            ViewBag.Baslik = "Taziye Listesi";
            var taziyeListesi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Onay.Equals((byte)Enums.HaberDurumu.Onaylanmis)).OrderBy(x => x.Tarih);
            taziyeListesi.Reverse();
            return View(taziyeListesi);
        }
        public IActionResult TaziyeDetayi(string? id)
        {
            DateTime Tarih = Convert.ToDateTime(id);
            if (id == null)
            {
                return NotFound();
            }
            var entity = _service.FindAll(x=>x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Tarih.Equals(Tarih));
            if (entity == null)
            {
                return NotFound();//Bulunamadı
            }
            ViewBag.Tarih = id;
            return View(entity);
        }


        [AutFilter]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Baslik = "Olum Listesi";
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif)); ;
            return View(entity);
        }
        [AutFilter]
        [HttpGet]
        public IActionResult YeniOlum()
        {
            ViewBag.Baslik = "Yeni Ölum";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public IActionResult YeniOlum(Olumler entity)
        {
            entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.DegistirmeTarihi = DateTime.Now;
            entity.KaydedenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.KayıtTarihi = DateTime.Now;
            entity.Durum = (byte)Enums.KayitDurumu.Aktif;
            _service.Save(entity);
            return RedirectToAction("Index", "Olum");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult OlumGuncelle(int? id)
        {
            ViewBag.Baslik = "Ölüm Düzenle";
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
        [AutFilter]
        [HttpPost]
        public IActionResult OlumGuncelle(Olumler entity)
        {
            ViewBag.Baslik = "Taziye Güncelle";
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
        public IActionResult OlumDetaylar(int? id)
        {
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
        public JsonResult OlumSil(int pId)
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
