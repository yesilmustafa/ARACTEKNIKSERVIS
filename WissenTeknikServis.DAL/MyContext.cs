using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.Entities;
using WissenTeknikServis.Entity.IdentityModels;

namespace WissenTeknikServis.DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public MyContext() : base("mycon")
        {

        }
        public DbSet<Anket> Anketler { get; set; }
        public DbSet<Soru> Sorular { get; set; }
        public DbSet<Cevap> Cevaplar { get; set; }
        public DbSet<Ariza> Arizalar { get; set; }
        public DbSet<Fotograf> Fotograflar { get; set; }
        public DbSet<ArizaDurumu> ArizaDurumlari { get; set; }
      
    }
}
    



