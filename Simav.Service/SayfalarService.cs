using Simav.Data;
using Simav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simav.Service
{
    public class SayfalarService
    {
        SayfalarRepository _repository = new SayfalarRepository();
        public List<Sayfalar> SayfalariGetir(byte SayfaTipi)
        {
            return _repository.SayfalariGetir(SayfaTipi);
        }
    }
}
