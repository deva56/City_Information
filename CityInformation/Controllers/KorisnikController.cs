using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CityInformation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity.Infrastructure;
using System.IO;
using Microsoft.Owin.Security;

namespace CityInformation.Controllers
{
    [Authorize(Roles = "Korisnik")]
    public class KorisnikController : Controller
    {
        private ModelsContext db = new ModelsContext();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //upload slika i spremanje u bazu - putanja ide u bazu, slika se sprema u folder aplikacije
        [HttpPost]
        public ActionResult UpravljanjeSlikomProfila(UploadImagesModels uploadImagesModels)
        {
            string idKorisnika = User.Identity.GetUserId();

            if (uploadImagesModels.ImageFile[0] == null)
            {
                TempData["shortMessage"] = "Niste odabrali niti jednu sliku...";
                return RedirectToAction("Profil", new { id = idKorisnika });
            }
            else
            {
                Random rnd = new Random();
                int brojGreski = 0;

                foreach (HttpPostedFileBase file in uploadImagesModels.ImageFile)
                {
                    string extension = Path.GetExtension(file.FileName);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream, true, true);
                    int width = image.Width;
                    int height = image.Height;
                    if ((extension == ".png" || extension == ".jpg" || extension == ".bmp" || extension == ".jpeg") && width >= 1080 && width <= 2160 && height >= 800 && height <= 1440)
                    {
                        //
                    }
                    else
                    {
                        brojGreski++;
                    }
                }

                if (brojGreski > 0)
                {
                    TempData["shortMessage"] = "Dopuštene su samo slike .png, .jpg ili .bmp formata i moraju biti minimalno 1080x800 te maksimalno 2160x1440 piksela...";
                    return RedirectToAction("Profil", new { id = idKorisnika });
                }
                else
                {
                    foreach (HttpPostedFileBase file in uploadImagesModels.ImageFile)
                    {

                        string folderPath = "~/Images/";
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        korisnik korisnik = db.korisnik.Find(User.Identity.GetUserId());
                        string putanjaSlike = Server.MapPath(korisnik.putanjaDoProfilneSlike);
                        if (System.IO.File.Exists(putanjaSlike))
                        {
                            System.IO.File.Delete(putanjaSlike);
                        }
                        korisnik.putanjaDoProfilneSlike = folderPath + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(fileName);
                        db.SaveChanges();
                    }

                    TempData["shortMessage"] = "Slika uspješno uploadana...";
                    return RedirectToAction("Profil", new { id = idKorisnika });
                }
            }
        }

        //korisnički profil koji vidi svaki korisnik
        public ActionResult Profil()
        {
            korisnik korisnik = db.korisnik.Find(User.Identity.GetUserId());
            UploadImagesModels uploadSlika = new UploadImagesModels();
            Tuple<korisnik, UploadImagesModels> tuple = Tuple.Create(korisnik, uploadSlika);

            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
                TempData.Remove("shortMessage");
            }
            return View(tuple);
        }

        //mjenjanje lozinke - get
        public async Task<ActionResult> ChangePassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aspnetuser aspnetusers = await db.aspnetusers.FindAsync(id);
            if (aspnetusers == null)
            {
                return HttpNotFound();
            }
            PromijeniLozinku promijeniLozinku = new PromijeniLozinku { ID = id };
            return View("~/Views/Korisnik/ChangePassword.cshtml", promijeniLozinku);
        }

        //mjenjanje lozinke, zapisivanje u bazu - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Include = "ID,StaraLozinka,NovaLozinka,PotvrdiNovuLozinku")] PromijeniLozinku promijeniLozinku)
        {
            if (ModelState.IsValid)
            {
                var user = await db.aspnetusers.FindAsync(promijeniLozinku.ID);

                if (!string.IsNullOrWhiteSpace(promijeniLozinku.StaraLozinka) && !string.IsNullOrWhiteSpace(promijeniLozinku.NovaLozinka))
                {
                    if (UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, promijeniLozinku.StaraLozinka) == PasswordVerificationResult.Success)
                    {
                        if (UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, promijeniLozinku.NovaLozinka) == PasswordVerificationResult.Success)
                        {
                            ModelState.AddModelError(string.Empty, "Nova i stara lozinka moraju biti različite!");
                            PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                            return View("~/Views/Korisnik/ChangePassword.cshtml", _promijeniLozinku);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Stara lozinka nije ispravna!");
                        PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                        return View("~/Views/Korisnik/ChangePassword.cshtml", _promijeniLozinku);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lozinka ne može sadržavati samo razmak u sebi ili biti prazna!");
                    PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                    return View("~/Views/Korisnik/ChangePassword.cshtml", _promijeniLozinku);
                }

                try
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(promijeniLozinku.NovaLozinka);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.aspnetusers.Any(e => e.Id == promijeniLozinku.ID))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Profil");
            }
            PromijeniLozinku promijeniLozinku_ = new PromijeniLozinku();
            return View("~/Views/Korisnik/ChangePassword.cshtml", promijeniLozinku_);
        }

        //brisanje profila - get
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            korisnik korisnik = await db.korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }

        //brisanje profila - post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            korisnik korisnik = await db.korisnik.FindAsync(id);

            if(korisnik.putanjaDoProfilneSlike != null)
            {
                string putanjaSlike = Server.MapPath(korisnik.putanjaDoProfilneSlike);

                if (System.IO.File.Exists(putanjaSlike))
                {
                    System.IO.File.Delete(putanjaSlike);
                }
            }

            aspnetuser aspnetuser = await db.aspnetusers.FindAsync(id);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            db.aspnetusers.Remove(aspnetuser);
            db.korisnik.Remove(korisnik);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
