using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.Entities;

namespace WissenTeknikServis.BLL.Repository
{
   public class Repository
    {
        public class AnketRepo : RepositoryBase<Anket, int> { }
        public class SoruRepo : RepositoryBase<Soru, int> { }
        public class CevapRepo : RepositoryBase<Cevap, int> { }
        public class ArizaRepo : RepositoryBase<Ariza, int> { }
        public class ArizaDurumRepo : RepositoryBase<ArizaDurumu, int> { }
        public class FotografRepo : RepositoryBase<Fotograf, int> { }
      
    }
}
