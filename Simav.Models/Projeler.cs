using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Projeler
    {
        public int ProjeId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string Ad { get; set; }
        public string KısaAciklama { get; set; }
        public string Icerik { get; set; }
        public int? Sira { get; set; }
    }
}
