using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Personel
    {
        public int PersonelId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string AdSoyad { get; set; }
        public byte? Unvan { get; set; }
        public long? Tel { get; set; }
        public string Email { get; set; }
        public string Cv { get; set; }
        public string Fb { get; set; }
        public string Tw { get; set; }
    }
}
