using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Etkinlikler
    {
        public int EtkinlikId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string EtkinlikAdi { get; set; }
        public string KisaAciklama { get; set; }
        public string Icerik { get; set; }
        public string Resim { get; set; }
    }
}
