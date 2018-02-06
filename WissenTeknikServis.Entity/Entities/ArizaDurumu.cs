using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.Enums;

namespace WissenTeknikServis.Entity.Entities
{
    [Table("ArizaDurumlar")]
    public class ArizaDurumu : BaseEntity<int>
    {
        public int ArizaID { get; set; }
        [Required]
        public ArizaDurumlari Durum { get; set; }
        public string Aciklama { get; set; }

        [ForeignKey("ArizaID")]
        public Ariza Ariza { get; set; }

    }
}
