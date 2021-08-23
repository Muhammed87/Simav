using App.Common.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simav.Common;
using Simav.Core;
using Simav.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Simav.Controllers
{
    public class MeclisGundemController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IService<MeclisGundem> _service;
        public MeclisGundemController(IService<MeclisGundem> service, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _service = service;
        }
        public IActionResult GundemListesi()
        {
            ViewBag.Baslik = "Gündem Listesi";
            var haberListesi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif));
            return View(haberListesi);
        }
        public IActionResult MeclisGundemListesi()
        {
            ViewBag.Baslik = "Gündem Listesi";
            var haberListesi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.GundemTuru.Equals((byte)Enums.GundemTuru.GundemMaddeleri));
            return View(haberListesi);
        }
        public IActionResult MeclisKararListesi()
        {
            var haberListesi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.GundemTuru.Equals((byte)Enums.GundemTuru.GundemKararlari));
            return View(haberListesi);
        }
        [AutFilter]
        public IActionResult GundemDetayi(int? id)
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
        [AutFilter]
        public IActionResult Index()
        {
            ViewBag.Baslik = "Gündem Listesi";
            var haberListesi = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif));
            return View(haberListesi);
        }
        [HttpGet]
        [AutFilter]
        public IActionResult YeniGundem()
        {
            ViewBag.Baslik = "Yeni Gundem";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public async Task<IActionResult> YeniGundem(MeclisGundem entity, IFormFile uploaded_File)
        {
            var fileName = Path.GetFileName(uploaded_File.FileName);
            var extension = Path.GetExtension(fileName);
            if (uploaded_File == null || uploaded_File.Length == 0)
            {
                ModelState.AddModelError("", "Dosya Seçilmedi!");
                return View();
            }
            if (!string.Equals(".pdf", extension))

            {
                ModelState.AddModelError("", "Dosya Türü Yanlış Seçildi!");
                return View();
            }
            string sImage_Folder = "MeclisGundem_Dosyalari";
            string sTarget_Filename = entity.GundemAdi + "_" + DateTime.Now.ToString().Replace(" ", string.Empty).Replace(":", string.Empty) + ".pdf";
            string sPath_WebRoot = _hostEnvironment.WebRootPath;
            string sPath_of_Target_Folder = sPath_WebRoot + "\\pdf\\" + sImage_Folder + "\\";
            string sFile_Target_Original = sPath_of_Target_Folder + sTarget_Filename;
            using (var stream = new FileStream(sFile_Target_Original, FileMode.Create))
            {
                await uploaded_File.CopyToAsync(stream);
            }
            entity.DosyaYolu = sTarget_Filename;
            entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.DegistirmeTarihi = DateTime.Now;
            entity.KaydedenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.KayıtTarihi = DateTime.Now;
            _service.Save(entity);
            return RedirectToAction("Index", "MeclisGundem");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult GundemDetaylar(int? id)
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
        [AutFilter]
        [HttpGet]
        public IActionResult GundemGuncelle(int? id)
        {
            ViewBag.Baslik = "Gundem Düzenle";
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
        [HttpPost]
        public async Task<IActionResult> GundemGuncelleAsync(MeclisGundem entity, IFormFile uploaded_File)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(uploaded_File.FileName);
                var extension = Path.GetExtension(fileName);
                if (uploaded_File == null || uploaded_File.Length == 0)
                {
                    ModelState.AddModelError("", "Dosya Seçilmedi!");
                    return View();
                }
                if (!string.Equals(".pdf", extension))

                {
                    ModelState.AddModelError("", "Dosya Türü Yanlış Seçildi!");
                    return View();
                }
                string sImage_Folder = "MeclisGundem_Dosyalari";
                string sTarget_Filename = entity.GundemAdi + "_" + DateTime.Now.ToString().Replace(" ", string.Empty).Replace(":", string.Empty) + ".pdf";
                string sPath_WebRoot = _hostEnvironment.WebRootPath;
                string sPath_of_Target_Folder = sPath_WebRoot + "\\pdf\\" + sImage_Folder + "\\";
                string sFile_Target_Original = sPath_of_Target_Folder + sTarget_Filename;
                using (var stream = new FileStream(sFile_Target_Original, FileMode.Create))
                {
                    await uploaded_File.CopyToAsync(stream);
                }
                entity.DosyaYolu = sTarget_Filename;
                entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
                entity.DegistirmeTarihi = DateTime.Now;
                _service.Update(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }
        public JsonResult GundemSil(int pId)
        {
            try
            {
                var haber = _service.GetById(pId);
                if (haber == null)
                {
                    return Json(new { basarili = false, id = pId, mesaj = "İşlem Başarısız" });
                }
                haber.Durum = (byte)Enums.KayitDurumu.Silinmiş;
                _service.Update(haber);
            }
            catch (Exception)
            {
                return Json(new { basarili = false, id = pId, mesaj = "İşlem Başarısız" });
            }
            return Json(new { basarili = true, id = pId, mesaj = "İşlem Başarılı" });
        }
    }
}
