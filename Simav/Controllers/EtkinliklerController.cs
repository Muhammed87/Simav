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
    public class EtkinliklerController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IService<Etkinlikler> _service;
        public EtkinliklerController(IService<Etkinlikler> service, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _service = service;
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
        public IActionResult YeniEtkinlik()
        {
            ViewBag.Baslik = "Yeni Etkinlik";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public async Task<IActionResult> YeniEtkinlikAsync(Etkinlikler entity, IFormFile uploaded_File)
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
            string sImage_Folder = "Etkinlik_Image";
            string sTarget_Filename = "Etkinlik_Image_" + DateTime.Now.ToString().Replace(" ", string.Empty).Replace(":", string.Empty) + ".jpg";
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
            entity.KaydedenKulId = SessionInfo.GirisYapanKullaniciId;
            entity.KayıtTarihi = DateTime.Now;
            entity.Durum = (byte)Enums.KayitDurumu.Aktif;
            _service.Save(entity);
            return RedirectToAction("Index", "Etkinlikler");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult EtkinlikGuncelle(int? id)
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
        public async Task<IActionResult> EtkinlikGuncelleAsync(Etkinlikler entity, IFormFile uploaded_File)
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
                    ModelState.AddModelError("", "Resim Seçilmedi!");
                    return View();
                }
                string sImage_Folder = "Etkinlik_Image";
                string sTarget_Filename = "Etkinlik_Image_" + DateTime.Now.ToString().Replace(" ", string.Empty).Replace(":", string.Empty) + ".jpg";
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
        [AutFilter]
        [HttpGet]
        public IActionResult EtkinlikDetaylar(int? id)
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
        public JsonResult EtkinlikSil(int pId)
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
