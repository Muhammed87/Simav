using Microsoft.EntityFrameworkCore;
using Simav.Common;
using Simav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simav.Data
{
    public class SayfalarRepository
    {
        AppDBContext _context = new AppDBContext();
        public List<Sayfalar> SayfalariGetir(byte SayfaTipi)
        {
            return _context.Sayfalars.Where(x => x.Durum.Equals((byte)Enums.KayitDurumu.Aktif) && x.SayfaTipi.Equals(SayfaTipi)).ToList();
        }
    }
}
