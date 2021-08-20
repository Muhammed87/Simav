using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Sayfalar
    {
        public int SayfaId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string SayfaAdi { get; set; }
        public byte? SayfaTipi { get; set; }
        public string Icerik { get; set; }
        public string KisaAciklama { get; set; }
        public byte? Yayınla { get; set; }
    }
}
