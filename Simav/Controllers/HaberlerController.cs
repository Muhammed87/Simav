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
      public class HaberlerController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IService<Haberler> _service;
        public HaberlerController(IService<Haberler> service,IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _service = service;
        }
        
        public IActionResult HaberListesi()
        {
            ViewBag.Baslik = "Haber Listesi";
            var haberListesi = _service.FindAll(x=>x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Onay.Equals((byte)Enums.HaberDurumu.Onaylanmis));
            return View(haberListesi);
        }
        public IActionResult HaberDetayi(int? id)
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
            ViewBag.Baslik = "Haber Listesi";
            var haberListesi = _service.FindAll(x=>x.Durum.Equals((byte)Enums.KayitDurumu.Aktif));
            return View(haberListesi);
        }
        [HttpGet]
        [AutFilter]
        public IActionResult YeniHaber()
        {
            ViewBag.Baslik = "Yeni Haber";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public async Task<IActionResult> YeniHaber(Haberler entity, IFormFile uploaded_File)
        {
            if (uploaded_File == null || uploaded_File.Length == 0)
            {
                ModelState.AddModelError("", "Resim Seçilmedi!");
                return View();
            }
            if (uploaded_File.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)

            {
                ModelState.AddModelError("", "Resim Seçilmedi!");
                return View();
            }
            string sImage_Folder =  "Haber_Image";
            string sTarget_Filename = "Haber_Image_" + DateTime.Now.ToString().Replace(" ", string.Empty).Replace(":", string.Empty) + ".jpg";
            string sPath_WebRoot = _hostEnvironment.WebRootPath;
            string sPath_of_Target_Folder = sPath_WebRoot + "\\images\\" + sImage_Folder + "\\";
            string sFile_Target_Original = sPath_of_Target_Folder + sTarget_Filename;
            using (var stream = new FileStream(sFile_Target_Original, FileMode.Create))
            {
                await uploaded_File.CopyToAsync(stream);
            }
            entity.Resim= sTarget_Filename;
            entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.DegistirmeTarihi = DateTime.Now;
            entity.KaydedenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.KayıtTarihi = DateTime.Now;
            _service.Save(entity);
            return RedirectToAction("Index", "Haberler");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult HaberDetaylar(int? id)
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
        public IActionResult HaberGuncelle(int? id)
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
        [HttpPost]
        public async Task<IActionResult> HaberGuncelleAsync(Haberler entity, IFormFile uploaded_File)
        {
            if (ModelState.IsValid)
            {
                if (uploaded_File == null || uploaded_File.Length == 0)
                {
                    ModelState.AddModelError("", "Resim Seçilmedi!");
                    return View();
                }
                if (uploaded_File.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)

                {
                    ModelState.AddModelError("", "Resim Türü Yanlış Seçildi!");
                    return View();
                }
                string sImage_Folder = "Haber_Image";
                string sTarget_Filename = "Haber_Image_" + DateTime.Now.ToString().Replace(" ",string.Empty).Replace(":",string.Empty) + ".jpg";
                string sPath_WebRoot = _hostEnvironment.WebRootPath;
                string sPath_of_Target_Folder = sPath_WebRoot + "\\images\\" + sImage_Folder + "\\";
                string sFile_Target_Original = sPath_of_Target_Folder + sTarget_Filename;

                using (var stream = new FileStream(sFile_Target_Original, FileMode.Create))
                {
                    await uploaded_File.CopyToAsync(stream);
                }
                entity.Resim = sTarget_Filename;
                entity.DegistirenKulId = SessionInfo.GirisYapanKullaniciId;
                entity.DegistirmeTarihi = DateTime.Now;
                _service.Update(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }
        public JsonResult HaberSil(int pId)
        {
            try
            {
                var haber=_service.GetById(pId);
                if (haber == null)
                {
                    return Json(new { basarili = false, id=pId ,mesaj = "İşlem Başarısız" });
                }
                haber.Durum = (byte)Enums.KayitDurumu.Silinmiş;
                _service.Update(haber);
            }
            catch (Exception)
            {
                return Json(new { basarili = false, id = pId, mesaj = "İşlem Başarısız" });
            }
           return Json(new { basarili = true, id = pId , mesaj = "İşlem Başarılı" }); 
        }
    }
}
