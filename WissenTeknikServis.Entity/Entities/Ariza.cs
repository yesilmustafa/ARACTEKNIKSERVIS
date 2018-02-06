using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.IdentityModels;

namespace WissenTeknikServis.Entity.Entities
{
    [Table("Arizalar")]
    public class Ariza : BaseEntity<int>
    {
      
        [Display(Name ="Açıklama")]
        [StringLength(400, MinimumLength = 3, ErrorMessage = "Açıklama bölümü boş geçilemez!")]
        public string Aciklama { get; set; }
        public string UserID { get; set; }
        public string OperatorID { get; set; }
        public string TeknisyenID { get; set; }
        [Required]
        [Display(Name ="Başlık")]

        public string Baslik { get; set; }
        public string Rapor { get; set; }
        [Display(Name ="Firma Adı")]
        public string FirmaAdi { get; set; }
        [Required]
        public DateTime IslemTarihi { get; set; }
        [Display(Name = "Adres")]
        public string AdresAciklamasi { get; set; }

        public List<ArizaDurumu> Durumlar { get; set; } = new List<ArizaDurumu>();
        public List<Fotograf> Fotograflar { get; set; } = new List<Fotograf>();
      
     
        public ApplicationUser User { get; set; }
        [ForeignKey("OperatorID")]
        public ApplicationUser Operator { get; set; }
        [ForeignKey("TeknisyenID")]
        public ApplicationUser Teknisyen { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}