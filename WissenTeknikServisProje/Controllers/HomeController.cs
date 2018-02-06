using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WissenTeknikServis.BLL.Settings;
using WissenTeknikServis.Entity.Entities;
using WissenTeknikServis.Entity.Enums;
using WissenTeknikServis.Entity.ViewModel;
using static WissenTeknikServis.BLL.Repository.Repository;

namespace WissenTeknikServisProje.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkımızda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult Deneme()
        {
            return View();
        }
        public ActionResult Maintenance()
        {
            return View();
        }
        public ActionResult Location()
        {
            return View();
        }
        public ActionResult GenelDeneme()
        {
            return View();
        }
        [Authorize]
        public ActionResult ArizaKaydi()
        {
            ViewBag.Title = "Ariza  Formu | WissenTeknikServis";
            
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize]
        public ActionResult ArizaKayit(ArizaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Ariza Kayit Formu | Wissen Teknik Servis";

                
                return View(model);
            }
            if (model == null)
                return RedirectToAction("ArizaKaydi");
            Ariza yeniAriza = new Ariza()
            {
                UserID = User.Identity.GetUserId(),
                Aciklama = model.Aciklama,
                AdresAciklamasi = model.AdresAciklamasi,
                Baslik = model.Baslik,
                lat = model.lat,
                lng = model.lng,
                IslemTarihi = DateTime.Now
            };
            try
            {
                new ArizaRepo().Insert(yeniAriza);
                if (model.Fotograflar.Any())
                {
                    foreach (var dosya in model.Fotograflar)
                    {
                        if (dosya != null && dosya.ContentType.Contains("image") && dosya.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(dosya.FileName);
                            string extName = Path.GetExtension(dosya.FileName);
                            fileName = SiteSettings.UrlFormatConverter(fileName);
                            fileName += Guid.NewGuid().ToString().Replace("-", "");
                            var directoryPath = Server.MapPath("~/Uploads/ArizaImage");
                            var filePath = Server.MapPath("~/Uploads/ArizaImage/") + fileName + extName;
                            if (!Directory.Exists(directoryPath))
                                Directory.CreateDirectory(directoryPath);
                            dosya.SaveAs(filePath);
                            ResimBoyutlandir(400, 400, filePath);
                            new FotografRepo().Insert(new Fotograf()
                            {
                                ArızaID = yeniAriza.ID,
                                URL = @"/Uploads/ArizaImage/" + fileName + extName
                            });
                        }
                    }
                }
                ArizaDurumu yeniArizaDurum = new ArizaDurumu() //Ariza durumunu oluşturuldu olarak ekle
                {
                    Durum = ArizaDurumlari.Olusturuldu,
                    Aciklama = "Arıza kullanıcı tarafından oluşturuldu.",
                    ArizaID = yeniAriza.ID
                };
                yeniAriza.Durumlar.Add(yeniArizaDurum);
                new ArizaRepo().Update();
                RedirectToAction("ArizaKaydi");
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = "Arıza kaydedilirken beklenmeyen bir hata oluştu. > " + ex.Message;
                return RedirectToAction("ArizaKaydi");
            }
            ViewBag.Title = "Arıza Kayit Formu | Wissen Teknik Servis";
            return RedirectToAction("ArizaKaydi");
        }

        public void ResimBoyutlandir(int en, int boy, string yol)
        {
            WebImage img = new WebImage(yol);
            img.Resize(en, boy, false);
            img.AddTextWatermark("KLYTeknikServis", opacity: 50, fontColor: "Tomato", fontSize: 18, fontFamily: "Verdana");
            img.Save(yol);
        }

        public PartialViewResult HeaderResult()
        {

            return PartialView("_PartialHeader");
        }
        public PartialViewResult SliderResult()
        {

            return PartialView("_PartialSlider");
        }

        public PartialViewResult ContentResult()
        {

            return PartialView("_PartialContent");
        }
        public PartialViewResult FooterResult()
        {

            return PartialView("_PartialFooter");
        }
    }
}