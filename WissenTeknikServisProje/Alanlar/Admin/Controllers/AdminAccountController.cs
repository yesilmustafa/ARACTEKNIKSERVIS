using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WissenTeknikServis.BLL.Account;
using WissenTeknikServis.BLL.Settings;
using WissenTeknikServis.Entity.Enums;
using WissenTeknikServis.Entity.IdentityModels;
using WissenTeknikServis.Entity.ViewModel;

namespace WissenTeknikServisMvcProje.UI.Alanlar.Admin.Controllers
{
    public class AdminAccountController : WissenTeknikServisProje.Controllers.AccountController
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public override ActionResult Register()
        {
            return base.Register();
        }
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var userManager = MembershipTools.NewUserManager();

            var checkUser = userManager.FindByName(model.Username);
            if (checkUser != null)
            {
                ModelState.AddModelError(string.Empty, "Bu kullanıcı adı daha önceden kayıt edilmiş");
                return View(model);
            }

            var activationCode = Guid.NewGuid().ToString().Replace("-", "");
            var user = new ApplicationUser()
            {
                Name = model.Name,
                SurName = model.Surname,
                Email = model.Email,
                UserName = model.Username,
                ActivationCode = activationCode
            };

            var sonuc = userManager.Create(user, model.Password);
            if (sonuc.Succeeded)
            {
                if (userManager.Users.Count() == 1)
                    userManager.AddToRole(user.Id, IdentityRoles.Admin.ToString());
                else
                    userManager.AddToRole(user.Id, model.Role.ToString());

                string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                                 (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                await SiteSettings.SendMail(new MailModel()
                {
                    To = user.Email,
                    Subject = "UyelikDB - Aktivasyon",
                    Message = $"Merhaba {user.Name} {user.SurName} <br/>Hesabınızı aktifleştirmek için <b><a href='{siteUrl}/Account/Activation?code={activationCode}'>Aktivasyon Kodu</a></b> tıklayınız."
                });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı kayıt işleminde hata oluştu!");
                return View(model);
            }
        }

    }
}