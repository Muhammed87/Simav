using System;
using System.Collections.Generic;

#nullable disable

namespace Simav.Models
{
    public partial class Sikayetler
    {
        public int SikayetId { get; set; }
        public byte Durum { get; set; }
        public DateTime KayıtTarihi { get; set; }
        public int KaydedenKulId { get; set; }
        public int DegistirenKulId { get; set; }
        public DateTime DegistirmeTarihi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public long? Tel { get; set; }
        public string Icerik { get; set; }
        public byte? Oku { get; set; }
        public string Ip { get; set; }
        public DateTime? Tarih { get; set; }
    }
}
