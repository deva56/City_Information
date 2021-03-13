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
using System.IO;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity.Infrastructure;

namespace CityInformation.Controllers
{
    [Authorize(Roles = "Poduzeće")]
    public class PoduzećeController : Controller
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

        //dodavanje slika u bazu
        [HttpPost]
        public ActionResult UpravljanjeSlikomProfila(UploadImagesModels uploadImagesModels)
        {
            string idPoduzeća = User.Identity.GetUserId();

            if (uploadImagesModels.ImageFile[0] == null)
            {
                TempData["shortMessage"] = "Niste odabrali niti jednu sliku...";
                return RedirectToAction("Profil", new { id = idPoduzeća });
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
                    return RedirectToAction("Profil", new { id = idPoduzeća });
                }
                else
                {
                    foreach (HttpPostedFileBase file in uploadImagesModels.ImageFile)
                    {

                        string folderPath = "~/Images/";
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string putanjaSlike = Server.MapPath(fileName);
                        if (System.IO.File.Exists(putanjaSlike))
                        {
                            System.IO.File.Delete(putanjaSlike);
                        }
                        slika slika = new slika { putanjaSlike = folderPath + fileName, idSlika = DateTime.Now.ToString("yymmssfff") + rnd.Next(1, 100000000).ToString(), idPoduzeća = User.Identity.GetUserId() };
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(fileName);
                        db.slika.Add(slika);
                        db.SaveChanges();
                    }

                    TempData["shortMessage"] = "Slika uspješno uploadana...";
                    return RedirectToAction("Profil", new { id = idPoduzeća });
                }
            }
        }

        //brisanje slike iz baze - get view
        public async Task<ActionResult> ObrišiSliku(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slike = await (from c in db.slika where c.idPoduzeća.Equals(id) select c).ToListAsync();

            if(slike.Count == 0)
            {
                TempData["status"] = "Nemate nikakvih slika za brisanje.";
                return RedirectToAction("Profil");
            }

            return View("~/Views/Poduzeće/ObrišiSliku.cshtml", slike);
        }
        
        //brisanje slika iz baze - post
        public async Task<ActionResult> ObrišiSlikuConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            slika slika = db.slika.Find(id);

            string putanjaSlike = Server.MapPath(slika.putanjaSlike);


            if (System.IO.File.Exists(putanjaSlike))
            {
                System.IO.File.Delete(putanjaSlike);
            }

            db.slika.Remove(slika);
            await db.SaveChangesAsync();
            
            var slike = await (from c in db.slika where c.idPoduzeća.Equals(id) select c).ToListAsync();

            if (slike.Count == 0)
            {
                return RedirectToAction("Profil");
            }
            else
            {
                return RedirectToAction("ObrišiSliku");
            }
        }

        //profil poduzeća
        public ActionResult Profil()
        {
            poduzeće poduzeće = db.poduzeće.Find(User.Identity.GetUserId());

            UploadImagesModels uploadSlika = new UploadImagesModels();
            IEnumerable<slika> modelSlika = from c in db.slika where c.idPoduzeća.Equals(poduzeće.idPoduzeće) select c;

            Tuple<IEnumerable<slika>, poduzeće, UploadImagesModels> tuple = Tuple.Create(modelSlika, poduzeće, uploadSlika);

            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
                TempData.Remove("shortMessage");
            }
            return View("~/Views/Poduzeće/Profil.cshtml", tuple);
        }

        //mjenanje lozinke - get
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
            return View("~/Views/Poduzeće/ChangePassword.cshtml", promijeniLozinku);
        }

        //mjenjanje lozinke - post
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
                            return View("~/Views/Poduzeće/ChangePassword.cshtml", _promijeniLozinku);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Stara lozinka nije ispravna!");
                        PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                        return View("~/Views/Poduzeće/ChangePassword.cshtml", _promijeniLozinku);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lozinka ne može sadržavati samo razmak u sebi ili biti prazna!");
                    PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                    return View("~/Views/Poduzeće/ChangePassword.cshtml", _promijeniLozinku);
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
            return View("~/Views/Poduzeće/ChangePassword.cshtml", promijeniLozinku_);
        }

        //ažuriranje podataka - get
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            poduzeće poduzeće = await db.poduzeće.FindAsync(id);
            if (poduzeće == null)
            {
                return HttpNotFound();
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            ViewBag.idUlica = new SelectList(db.ulica, "idUlica", "imeUlica");
            ViewBag.djelatnost = new SelectList(db.djelatnost, "idDjelatnost", "imeDjelatnost");
            return View(poduzeće);
        }

        //ažuriranje podataka - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idPoduzeće,imePoduzeće,opisPoduzeće,kontaktTelefon,kontaktEmail,tipPoslovnogObjekta,gradPoduzeće,županijaPoduzeće,ulicaPoduzeće,korisničkoImePoduzeće,javanPoduzeće")] poduzeće poduzeće)
        {
            if (ModelState.IsValid)
            {
                poduzeće _poduzeće = db.poduzeće.Find(poduzeće.idPoduzeće);

                if (poduzeće.tipPoslovnogObjekta != null)
                {
                    djelatnost djelatnost = db.djelatnost.Find((poduzeće.tipPoslovnogObjekta));
                    _poduzeće.tipPoslovnogObjekta = poduzeće.tipPoslovnogObjekta;
                }
                if (poduzeće.gradPoduzeće != null)
                {
                    grad grad = db.grad.Find((poduzeće.gradPoduzeće));
                    _poduzeće.gradPoduzeće = poduzeće.gradPoduzeće;
                }
                if (poduzeće.županijaPoduzeće != null)
                {
                    županija županija = db.županija.Find((poduzeće.županijaPoduzeće));
                    _poduzeće.županijaPoduzeće = poduzeće.županijaPoduzeće;
                }
                if (poduzeće.ulicaPoduzeće != null)
                {
                    ulica ulica = db.ulica.Find((poduzeće.ulicaPoduzeće));
                    _poduzeće.ulicaPoduzeće = poduzeće.ulicaPoduzeće;
                }
                _poduzeće.javanPoduzeće = poduzeće.javanPoduzeće;
                _poduzeće.imePoduzeće = poduzeće.imePoduzeće;
                _poduzeće.opisPoduzeće = poduzeće.opisPoduzeće;
                _poduzeće.kontaktEmail = poduzeće.kontaktEmail;
                _poduzeće.kontaktTelefon = poduzeće.kontaktTelefon;

                db.Entry(_poduzeće).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Profil");
            }
            return View(poduzeće);
        }

        //brisanje poduzeća - get
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            poduzeće poduzeće = await db.poduzeće.FindAsync(id);
            if (poduzeće == null)
            {
                return HttpNotFound();
            }
            return View(poduzeće);
        }

        //brisanje poduzeća - post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            poduzeće poduzeće = await db.poduzeće.FindAsync(id);
            var slike = await (from c in db.slika where c.idPoduzeća.Equals(id) select c).ToListAsync();

            if (slike.Count != 0)
            {
                foreach (slika slika in slike)
                {
                    slika s = db.slika.Find(slika.idSlika);

                    string putanjaSlike = Server.MapPath(s.putanjaSlike);

                    if (System.IO.File.Exists(putanjaSlike))
                    {
                        System.IO.File.Delete(putanjaSlike);
                    }

                    db.slika.Remove(s);
                    await db.SaveChangesAsync();
                }
            }

            aspnetuser aspnetuser = await db.aspnetusers.FindAsync(id);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            db.aspnetusers.Remove(aspnetuser);
            db.poduzeće.Remove(poduzeće);
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
