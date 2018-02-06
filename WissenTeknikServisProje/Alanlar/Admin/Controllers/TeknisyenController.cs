using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WissenTeknikServis.BLL.Account;
using WissenTeknikServis.BLL.Settings;
using WissenTeknikServis.Entity.Entities;
using WissenTeknikServis.Entity.Enums;
using WissenTeknikServis.Entity.ViewModel;
using static WissenTeknikServis.BLL.Repository.Repository;

namespace WissenTeknikServisProje.Alanlar.Admin.Controllers
{
    [Authorize(Roles = "Teknisyen")]
    public class TeknisyenController : Controller
    {
        // GET: Yonetim/Teknisyen
        public ActionResult Index()
        {
            var model = new ArizaRepo().GetAll().OrderByDescending(x => x.CreateDate).Where(x => x.TeknisyenID == User.Identity.GetUserId()).ToList();
            return View(model);
        }

        public PartialViewResult TeknisyenSideBar()
        {
            ViewBag.adsoyad = MembershipTools.GetNameSurname();
            return PartialView("_PartialTeknisyenSidebar");
        }

        public ActionResult ArizaDetay(int id)
        {
            var arizaRepo = new ArizaRepo();
            var userManager = MembershipTools.NewUserManager();
            string userId = arizaRepo.GetById(id).UserID;
            string operatorId = arizaRepo.GetById(id).OperatorID;
            string teknisyenId = arizaRepo.GetById(id).TeknisyenID;


            var model = new ArizaDetayViewModel            {
                FotografList = new FotografRepo().GetAll().Where(x => x.ArızaID == id).ToList(),
                Ariza = new ArizaRepo().GetById(id),
                DurumListesi = new ArizaDurumRepo().GetAll().Where(x => x.ArizaID == id).ToList(),
                Operator = userManager.Users.Where(x => x.Id == operatorId).FirstOrDefault(),
                Teknisyen = userManager.Users.Where(x => x.Id == teknisyenId).FirstOrDefault(),
                User = userManager.Users.Where(x => x.Id == userId).FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DurumEkle(string durumId, string durumIcerik, int? Ariza_ID, string user_ID, string teknisyen_ID)
        {

            var user = MembershipTools.NewUserManager().Users.Where(x => x.Id == user_ID).FirstOrDefault();
            var teknisyen = MembershipTools.NewUserManager().Users.Where(x => x.Id == teknisyen_ID).FirstOrDefault();
            int durumID = Convert.ToInt32(durumId);
            var arizaRepo = new ArizaRepo();
            arizaRepo.GetById(Ariza_ID.Value).Durumlar.Add(new ArizaDurumu()
            {
                ArizaID = Ariza_ID.Value,
                Durum = (ArizaDurumlari)durumID,
                Aciklama = durumIcerik
            });
            arizaRepo.Update();
            await SiteSettings.SendMail(new MailModel()
            {
                To = user.Email,
                Subject = $"KLY Teknik Servis - {(ArizaDurumlari)durumID}",
                Message = $"Merhaba {user.Name} {user.SurName}, <br/> Arıza talebinizin durumu Teknisyen {teknisyen.Name} {teknisyen.SurName} tarafından <b>{(ArizaDurumlari)durumID}</b> olarak güncellenmiştir.<br/> <b>Güncellemeye dair mesaj :</b>.{durumIcerik}"
            });
            return RedirectToAction("ArizaDetay", "Teknisyen", new { id = Ariza_ID });
        }


    }
}