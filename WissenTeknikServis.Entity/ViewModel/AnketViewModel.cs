using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.Entities;

namespace WissenTeknikServis.Entity.ViewModel
{
   public class AnketViewModel
    {
        public string anketadi { get; set; }
        public List<Soru> sorulistesi { get; set; }
    }
}
