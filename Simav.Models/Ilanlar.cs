using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Ilanlar
    {
        public int? IlanlarId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string Ad { get; set; }
        public string KısaAciklama { get; set; }
        public string Icerik { get; set; }
        public DateTime? Tarih { get; set; }
        public byte? Onay { get; set; }
    }
}
