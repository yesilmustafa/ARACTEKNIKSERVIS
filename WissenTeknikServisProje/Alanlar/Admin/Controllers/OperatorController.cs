using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WissenTeknikServis.BLL.Account;
using WissenTeknikServis.Entity.Entities;
using WissenTeknikServis.Entity.ViewModel;
using static WissenTeknikServis.BLL.Repository.Repository;

namespace WissenTeknikServisProje.Alanlar.Admin
{

    [Authorize(Roles = "Operator")]
    public class OperatorController : BaseController
    {
        // GET: Yonetim/Operator
        public ActionResult Index()
        {
            var model = new ArizaRepo().GetAll();
            return View(model);
        }
        public ActionResult ServisTalepListesi()
        {
            var model = new ArizaRepo().GetAll().OrderByDescending(x => x.CreateDate);
            return View(model);
        }

        public PartialViewResult OperatorSideBar()
        {
            ViewBag.adsoyad = MembershipTools.GetNameSurname();
            return PartialView("_PartialOperatorSidebar");
        }

        public ActionResult Detay(int id)
        {
            string arizaId = new ArizaRepo().GetById(id).UserID;
            var teknisyenler = TeknisyenSelectList();
            ViewBag.Teknisyenler = teknisyenler;
            var model = new ArizaDetayViewModel
            {

                Ariza = new ArizaRepo().GetById((int)id),
                DurumListesi = new ArizaDurumRepo().GetAll().Where(x => x.ArizaID == id).ToList(),
                User = MembershipTools.NewUserManager().Users.Where(x => x.Id == arizaId).FirstOrDefault()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GorevliAta(string TeknisyenID, int arizaID)
        {
            var seciliAriza = new ArizaRepo().GetById(arizaID);
            seciliAriza.TeknisyenID = TeknisyenID;
            seciliAriza.OperatorID = User.Identity.GetUserId();
            var userManager = MembershipTools.NewUserManager();
            string operatoradsoy = userManager.Users.Where(x => x.Id == seciliAriza.OperatorID).FirstOrDefault().Name + " " + userManager.Users.Where(x => x.Id == seciliAriza.OperatorID).FirstOrDefault().SurName;
            string teknisyenadsoy = userManager.Users.Where(x => x.Id == seciliAriza.TeknisyenID).FirstOrDefault().Name + " " + userManager.Users.Where(x => x.Id == seciliAriza.TeknisyenID).FirstOrDefault().SurName;
            new ArizaDurumRepo().Insert(new ArizaDurumu()
            {
                ArizaID = arizaID,
                Durum = WissenTeknikServis.Entity.Enums.ArizaDurumlari.Yonlendirildi,
                Aciklama = $"Arıza için {operatoradsoy}(Operatör) tarafından {teknisyenadsoy}(Teknisyen) görevlendirilmiştir."
            });
            new ArizaRepo().Update();
            return RedirectToAction("Index", "Operator");
        }

    }
}