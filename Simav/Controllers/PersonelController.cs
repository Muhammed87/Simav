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
    public class PersonelController : Controller
    {
        private readonly IService<Personel> _service;

        private readonly IWebHostEnvironment _hostEnvironment;
        public PersonelController(IService<Personel> service,IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _service = service;
        }
        public IActionResult BaskanYardimcilari()
        {
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Statu.Equals((byte)Enums.Statu.BelediyeBaskaniYardimcisi));
            return View(entity);
        }
        public IActionResult MeclisKomisyonlari()//TODO Meclis Komisyanlar işlemleri için Ayrı kontoller oluşturulması gerekir bunun için öncelikle gerekli tablolar ve modellerin oluşturulması gerekir.
        {

            return View();
        }
        public IActionResult Meclis()
        {
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Statu.Equals((byte)Enums.Statu.MeclisUyesi));
            return View(entity);
        }
        public IActionResult YonetimSemasi()
        {
            return View();
        }
        public IActionResult MuhtarListesi()
        {
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Statu.Equals((byte)Enums.Statu.Muhtar));
            return View(entity);
        }

        public IActionResult Mudurler()
        {
            var entity = _service.FindAll(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Statu.Equals((byte)Enums.Statu.Mudur));
            return View(entity);
        }
        public IActionResult EncumenListesi() //TODO: Burası için Tablo oluşturulması lazım ve ilgili tablodan bilgiler çekilmesi lazım
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
        public IActionResult YeniPersonel()
        {
            ViewBag.Baslik = "Yeni Personel";
            return View();
        }
        [AutFilter]
        [HttpPost]
        public async Task<IActionResult> YeniPersonelAsync(Personel entity, IFormFile uploaded_File)
        {
            if (uploaded_File == null || uploaded_File.Length == 0)
            {
                ModelState.AddModelError("", "Resim Seçilmedi!");
                return View();
            }
            if (uploaded_File.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)

            {
                ModelState.AddModelError("", "Resim Türü jpg olmadir Seçilmedi!");
                return View();
            }
            string sImage_Folder = "Personel_Image";
            string sTarget_Filename = "Personel_Image_" + DateTime.Now + ".jpg";
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
            return RedirectToAction("Index", "Personel");
        }
        [AutFilter]
        [HttpGet]
        public IActionResult PersonelGuncelle(int? id)
        {
            ViewBag.Baslik = "Personel Düzenle";
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
        public IActionResult PersonelGuncelle(Personel entity)
        {
            ViewBag.Baslik = "Personel Güncelle";
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
        public IActionResult PersonelDetaylar(int? id)
        {
            ViewBag.Baslik = "Personel Detayları";
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
        public JsonResult PersonelSil(int pId)
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
        public IActionResult BelediyeBaskani()
        {
            Personel entity = _service.Find(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.Statu.Equals((byte)Enums.Statu.BelediyeBaskani));
            return View(entity);
        }
    }
}
