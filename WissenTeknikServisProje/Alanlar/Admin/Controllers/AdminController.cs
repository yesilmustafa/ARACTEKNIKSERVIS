using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WissenTeknikServis.BLL.Account;
using WissenTeknikServis.Entity.Entities;
using WissenTeknikServis.Entity.Enums;
using WissenTeknikServis.Entity.IdentityModels;
using WissenTeknikServis.Entity.ViewModel;
using static WissenTeknikServis.BLL.Repository.Repository;

namespace WissenTeknikServisProje.Alanlar.Admin.Controllers
{
    
   [Authorize(Roles ="Admin")]
    public class AdminController:BaseController
    {
        // GET: Yonetim/Admin
        public ActionResult Index()
        {
            var model = new ArizaRepo().GetAll().OrderByDescending(x => x.CreateDate).Where(x => x.TeknisyenID == null).ToList();
            return View(model);
        }
        public ActionResult KullaniciListele()
        {
            var modelList = new List<GorevliListViewModel>();
            var userManager = MembershipTools.NewUserManager();
            userManager.Users.ToList()
                .ForEach(item => modelList.Add(new GorevliListViewModel()
                {
                    Name = item.Name,
                    Surname = item.SurName,
                    Email = item.Email,
                    Username = item.UserName,
                    ID = item.Id,
                    RoleId = item.Roles.First()?.RoleId
                }));
            var roller = MembershipTools.NewRoleManager().Roles.ToList();
            modelList.ForEach(x => x.Role = roller.FirstOrDefault(y => y.Id == x.RoleId)?.Name);

            return View(modelList);
        }
        public ActionResult KullaniciEkle()
        {
            var roleSelectList = RoleSelectList();
            ViewBag.roller = roleSelectList;

            return View();
        }

        //public object RoleSelectList()
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciEkle(GorevliViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.roller = RoleSelectList();
                return View(model);
            }


            var userManager = MembershipTools.NewUserManager();
            var checkUser = userManager.FindByName(model.Username);
            if (checkUser != null)
            {
                ModelState.AddModelError(string.Empty, "Bu kullanıcı adı daha önceden kayıt edilmiş");
                ViewBag.roller = RoleSelectList();
                return View(model);
            }

            var activationCode = Guid.NewGuid().ToString().Replace("-", "");
            var user = new ApplicationUser()
            {
                Name = model.Name,
                SurName = model.Surname,
                Email = model.Email,
                PhoneNumber = model.Telefon,
                UserName = model.Username,
                ActivationCode = activationCode
            };
            var sonuc = userManager.Create(user, model.Password);
            if (sonuc.Succeeded)
            {
                userManager.AddToRole(user.Id, MembershipTools.NewRoleManager().FindById(model.RoleID).Name);

                string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                                 (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);// BURASI eski adres ? 

                return RedirectToAction("KullaniciListele", "Admin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı kayıt işleminde hata oluştu!");
                ViewBag.roller = RoleSelectList();
                return View(model);
            }
        }

        public ActionResult KullaniciDuzenle(string id)
        {

            ViewBag.roller = RoleSelectList();
            var userManager = MembershipTools.NewUserManager();

            var seciliUser = userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            KullaniciDuzenleViewModel gorevli = new KullaniciDuzenleViewModel()
            {
                Email = seciliUser.Email,
                Name = seciliUser.Name,
                Surname = seciliUser.SurName,
                RoleID = seciliUser.Roles.FirstOrDefault(x => x.UserId == seciliUser.Id).RoleId,
                Username = seciliUser.UserName,
                Telefon = seciliUser.PhoneNumber,
                ID = seciliUser.Id
            };
            return View(gorevli);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult KullaniciDuzenle(KullaniciDuzenleViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var userManager = MembershipTools.NewUserManager();
            var seciliUser = userManager.Users.Where(x => x.Id == model.ID).FirstOrDefault();
            // var checkUser = userManager.FindByName(model.Username);
            //if (checkUser!=null)
            //{
            //    ModelState.AddModelError(string.Empty, "Bu kullanıcı adı daha önceden kayıt edilmiş!");
            //    return RedirectToAction("KullaniciDuzenle",new { id=model.ID});
            //}
            var eskirol = MembershipTools.NewRoleManager().FindById(seciliUser.Roles.FirstOrDefault().RoleId).Name;
            var yenirol = MembershipTools.NewRoleManager().FindById(model.RoleID).Name;
            userManager.RemoveFromRole(model.ID, eskirol);//eskirol silindi
            userManager.AddToRole(model.ID, yenirol); // rol eklendi
            //if (model.Password!=null)
            //{
            //    userManager.RemovePassword(model.ID);
            //    userManager.AddPassword(model.ID, model.Password);
            //}
            seciliUser.Name = model.Name;
            seciliUser.SurName = model.Surname;
            seciliUser.UserName = model.Username;
            seciliUser.PhoneNumber = model.Telefon;
            seciliUser.Email = model.Email;
            userManager.Update(seciliUser);
            return RedirectToAction("KullaniciListele", "Admin");
        }

        public ActionResult KullaniciSil(string id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var userManager = MembershipTools.NewUserManager();
            var seciliUser = userManager.Users.Where(x => x.Id == id).FirstOrDefault();


            return RedirectToAction("KullaniciListele", "Admin");
        }
  
        //[HttpPost]
        //[ValidateAntiForgeryToken]

       
      

        public ActionResult AnketEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnketEkle(AnketViewModel model)
        {
            var anketrepo = new AnketRepo();
            var sorurepo = new SoruRepo();
            Anket yeniAnket = new Anket()
            {
                AnketIsmi = model.anketadi
            };
            anketrepo.Insert(yeniAnket);

            model.sorulistesi.ForEach(x =>
            {
                sorurepo.Insert(new Soru()
                {
                    SoruMetni = x.SoruMetni,
                    AnketID = yeniAnket.ID,
                });
            });

            return View();
        }

        public ActionResult Ayarlar()
        {

            return View();
        }
    }
}