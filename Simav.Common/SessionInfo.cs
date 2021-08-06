using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simav.Common
{
    public class SessionInfo
    {
        private static int girisYapanKullaniciId;
        private static DateTime girisTarihi;
        private static string kullaniciAdSoyad;

        public static int GirisYapanKullaniciId { get => girisYapanKullaniciId; set => girisYapanKullaniciId = value; }
        public static DateTime GirisTarihi { get => girisTarihi; set => girisTarihi = value; }
        public static string KullaniciAdSoyad { get => kullaniciAdSoyad; set => kullaniciAdSoyad = value; }
    }
}
