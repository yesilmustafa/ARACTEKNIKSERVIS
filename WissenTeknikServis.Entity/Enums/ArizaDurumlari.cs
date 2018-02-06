using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WissenTeknikServis.Entity.Enums
{
    public enum ArizaDurumlari
    {
        [Display(Name = "Arıza Kaydınız Alındı.")]
        Olusturuldu,
        Yonlendirildi,
        Beklemede,
        [Display(Name = "Arıza Kaydınız iptal edildi")]
        IptalEdildi,
        ArizaGiderildi,
    }
}
