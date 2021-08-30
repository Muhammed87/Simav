using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simav.Common
{
   public class Enums
    {
        public enum HaberDurumu
        {
            OnayBekliyor = 0,
            Onaylanmis = 1,
        }
        public enum KayitDurumu
        {
            Aktif = 0,
            Silinmiş = 1,
        }
        public enum Statu
        {
            BelediyeBaskani = 1,
            BelediyeBaskaniYardimcisi = 2,
            MeclisUyesi = 3,
            Muhtar = 4,
            Mudur = 5,
            MudurYardimcisi = 6
        }
        public enum SayfaTipi
        {
            Kurum = 1,
            Ilcemiz = 2,
            Iletisim = 3,
            AnaSayfa = 4
        }
        public enum GundemTuru
        {
            GundemKararlari = 1,
            GundemMaddeleri = 2
        }
        public enum ProjeTuru
        {
            Istihdam = 1,
            Turizm = 2,
            Sehircilik=3,
            Egitim=4,
            Sosyal=5
        }
        public enum ProjeDurumu
        {
            Tamamlandi=1,
            tamamlanmadi=2
        }
    }
}
