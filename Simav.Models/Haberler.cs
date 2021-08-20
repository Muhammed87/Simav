using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Haberler
    {
        public int HaberlerId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string Ad { get; set; }
        public string KisaAciklama { get; set; }
        public string Icerik { get; set; }
        public string Resim { get; set; }
        public DateTime? Tarih { get; set; }
        public byte? Onay { get; set; }
    }
}
