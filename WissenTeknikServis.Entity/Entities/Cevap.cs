﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WissenTeknikServis.Entity.IdentityModels;

namespace WissenTeknikServis.Entity.Entities
{
    [Table("Cevaplar")]
    public class Cevap : BaseEntity<int>
    {
        public int? SoruCevap { get; set; }
        public int SoruID { get; set; }
        public string UserID { get; set; }
        [ForeignKey("SoruID")]
        public Soru Soru { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}
