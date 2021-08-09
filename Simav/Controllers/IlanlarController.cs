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
    public class IlanlarController : Controller
    {
        private readonly IService<Ilanlar> _service;
        public IlanlarController(IService<Ilanlar> service)
        {
            _service = service;
        }
        [AutFilter]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Baslik = "İlan Listesi";
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif)); ;
            return View(entity);
        }
        [AutFilter]
        [HttpGet]
        public IActionResult YeniIlan()
        {
            ViewBag.Baslik = "Yeni İlan";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public IActionResult YeniIlan(Ilanlar entity)
        {
            entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.DegistirmeTarihi = DateTime.Now;
            entity.KaydedenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.KayıtTarihi = DateTime.Now;
            entity.Durum = (byte)Enums.KayitDurumu.Aktif;
            _service.Save(entity);
            return RedirectToAction("Index", "Ilanlar");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult IlanGuncelle(int? id)
        {
            ViewBag.Baslik = "Haber Düzenle";
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
        public IActionResult IlanGuncelle(Ilanlar entity)
        {
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
        public IActionResult IlanDetaylar(int? id)
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
        public JsonResult IlanSil(int pId)
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
