using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Kullanicilar
    {
        public int KullaniciId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string KullaniciAdi { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long? Telefon { get; set; }
        public string Unvan { get; set; }
    }
}
