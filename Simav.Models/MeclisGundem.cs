using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class MeclisGundem
    {
        public int GundemId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string GundemAdi { get; set; }
        public byte? GundemTuru { get; set; }
        public string? DosyaYolu { get; set; }
        public string? KisaAciklama { get; set; }
        public byte? Yayinla { get; set; }
    }
}
