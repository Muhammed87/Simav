using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Olumler
    {
        public int OlumId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string AdSoyad { get; set; }
        public string KisaAciklama { get; set; }
        public string Icerik { get; set; }
        public DateTime? Tarih { get; set; }
        public byte? Onay { get; set; }
        public string Cami { get; set; }
        public string Mezarlik { get; set; }
        public TimeSpan? Saat { get; set; }
    }
}
