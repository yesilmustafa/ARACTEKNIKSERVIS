using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.Entities;
using WissenTeknikServis.Entity.IdentityModels;

namespace WissenTeknikServis.Entity.ViewModel
{
   
    public class ArizaDetayViewModel
    {
        public List<Fotograf> FotografList { get; set; }
        public Ariza Ariza { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser Teknisyen { get; set; }
        public ApplicationUser Operator { get; set; }
        public List<ArizaDurumu> DurumListesi { get; set; }
    }
}

