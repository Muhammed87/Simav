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
            MeclisUyesi=3,
            Muhtar=4,
            Mudur=5,
            MudurYardimcisi=6
        }
    }
}
