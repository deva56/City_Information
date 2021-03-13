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
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;

namespace CityInformation.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
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

        #region AdminRelated
        //glavni upravljački izbornik admina
        public ActionResult GlavniMeni()
        {
            return View("~/Views/Administrator/AdministratorRelated/GlavniMeni.cshtml");
        }

        //lista svih administratora u bazi
        public async Task<ActionResult> Index()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            return View("~/Views/Administrator/AdministratorRelated/Index.cshtml", await db.administrator.ToListAsync());
        }

        //kreiranje admina - get
        public ActionResult Create()
        {
            return View("~/Views/Administrator/AdministratorRelated/Create.cshtml");
        }

        //kreiranje admina - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Username,Password,ConfirmPassword")] RegisterViewModelAdmin registerViewModel)
        {
            RegisterViewModelAdmin _registerViewModel = new RegisterViewModelAdmin { };

            if (ModelState.IsValid)
            {
                var administratorPostoji = from c in db.aspnetusers where c.UserName.Equals(registerViewModel.Username) select c;

                if (administratorPostoji.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već postoji administrator sa istim imenom.");
                    return View("~/Views/Administrator/AdministratorRelated/Create.cshtml", _registerViewModel);

                }

                var user = new ApplicationUser { UserName = registerViewModel.Username, Email = registerViewModel.Username + "@cityinfo.com" };
                var result = await UserManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("Administrator"));
                    await UserManager.AddToRoleAsync(user.Id, "Administrator");

                    administrator administrator = new administrator { idAdministratora = user.Id, imeAdministratora = registerViewModel.Username };
                    db.administrator.Add(administrator);

                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");

                }
                else
                {
                    List<string> errori = new List<string>();
                    foreach (string errors in result.Errors)
                    {
                        errori.Add(errors);
                    }

                    foreach (string regerror in errori)
                    {
                        ModelState.AddModelError(string.Empty, regerror);
                    }
                }
                return View("~/Views/Administrator/AdministratorRelated/Create.cshtml", _registerViewModel);
            }

            return View("~/Views/Administrator/AdministratorRelated/Create.cshtml", _registerViewModel);
        }

        //mjenjanje lozinke admina - get
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
            return View("~/Views/Administrator/AdministratorRelated/ChangePassword.cshtml", promijeniLozinku);
        }

        //mjenjanje lozinke admina - post
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
                            return View("~/Views/Administrator/AdministratorRelated/ChangePassword.cshtml", _promijeniLozinku);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Stara lozinka nije ispravna!");
                        PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                        return View("~/Views/Administrator/AdministratorRelated/ChangePassword.cshtml", _promijeniLozinku);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lozinka ne može sadržavati samo razmak u sebi ili biti prazna!");
                    PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                    return View("~/Views/Administrator/AdministratorRelated/ChangePassword.cshtml", _promijeniLozinku);
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

                //db.Entry(aspnetusers).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PromijeniLozinku promijeniLozinku_ = new PromijeniLozinku();
            return View("~/Views/Administrator/AdministratorRelated/ChangePassword.cshtml", promijeniLozinku_);
        }

        //brisanje admina - get
        public async Task<ActionResult> Delete(string id)
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
            return View("~/Views/Administrator/AdministratorRelated/Delete.cshtml", aspnetusers);
        }

        //brisanje admina - post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            aspnetuser aspnetusers = await db.aspnetusers.FindAsync(id);
            administrator administrator = await db.administrator.FindAsync(id);

            if (User.Identity.GetUserId() == id)
            {
                TempData["status"] = "Match";
                return RedirectToAction("Index");
            }

            db.aspnetusers.Remove(aspnetusers);
            db.administrator.Remove(administrator);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region KorisnikRelated
        //lista korisnika u bazi
        public async Task<ActionResult> IndexKorisnik()
        {
            return View("~/Views/Administrator/KorisnikRelated/IndexKorisnik.cshtml", await db.korisnik.ToListAsync());
        }
        //mjenjanje lozinke korisnika - get
        public async Task<ActionResult> ChangePasswordKorisnik(string id)
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
            return View("~/Views/Administrator/KorisnikRelated/ChangePasswordKorisnik.cshtml", promijeniLozinku);
        }
        //mjenjanje lozinke korisnika - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePasswordKorisnik([Bind(Include = "ID,StaraLozinka,NovaLozinka,PotvrdiNovuLozinku")] PromijeniLozinku promijeniLozinku)
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
                            return View("~/Views/Administrator/KorisnikRelated/ChangePasswordKorisnik.cshtml", promijeniLozinku);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Stara lozinka nije ispravna!");
                        PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                        return View("~/Views/Administrator/KorisnikRelated/ChangePasswordKorisnik.cshtml", promijeniLozinku);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lozinka ne može sadržavati samo razmak u sebi ili biti prazna!");
                    PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                    return View("~/Views/Administrator/KorisnikRelated/ChangePasswordKorisnik.cshtml", promijeniLozinku);
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

                return RedirectToAction("IndexKorisnik");
            }
            PromijeniLozinku promijeniLozinku_ = new PromijeniLozinku();
            return View("~/Views/Administrator/KorisnikRelated/ChangePasswordKorisnik.cshtml", promijeniLozinku_);
        }
        //kreiranje korisnika - get
        public ActionResult CreateKorisnik()
        {
            return View("~/Views/Administrator/KorisnikRelated/CreateKorisnik.cshtml");
        }
        //kreiranje korisnika - post
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateKorisnik(RegisterViewModelKorisnik registerViewModel)
        {
            RegisterViewModelKorisnik _registerViewModel = new RegisterViewModelKorisnik { };

            if (ModelState.IsValid)
            {
                var korisnikPostoji = from c in db.aspnetusers where c.UserName.Equals(registerViewModel.Username) select c;

                if (korisnikPostoji.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već postoji korisnik sa istim imenom.");
                    return View("~/Views/Administrator/KorisnikRelated/CreateKorisnik.cshtml", _registerViewModel);

                }

                var user = new ApplicationUser { UserName = registerViewModel.Username, Email = registerViewModel.Email };
                var result = await UserManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("Korisnik"));
                    await UserManager.AddToRoleAsync(user.Id, "Korisnik");

                    korisnik korisnik = new korisnik { idKorisnik = user.Id, korisničkoImeKorisnik = registerViewModel.Username, registracijskiEmailKorisnik = registerViewModel.Email };
                    db.korisnik.Add(korisnik);

                    await db.SaveChangesAsync();

                    return RedirectToAction("IndexKorisnik");
                }
                else
                {
                    List<string> errori = new List<string>();
                    foreach (string errors in result.Errors)
                    {
                        errori.Add(errors);
                    }

                    foreach (string regerror in errori)
                    {
                        ModelState.AddModelError(string.Empty, regerror);
                    }
                }
                return View("~/Views/Administrator/KorisnikRelated/CreateKorisnik.cshtml", _registerViewModel);
            }

            return View("~/Views/Administrator/KorisnikRelated/CreateKorisnik.cshtml", _registerViewModel);
        }
        //brisanje korisnika - get
        public async Task<ActionResult> DeleteKorisnik(string id)
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
            return View("~/Views/Administrator/KorisnikRelated/DeleteKorisnik.cshtml", korisnik);
        }
        //brisanje korisnika - post
        [HttpPost, ActionName("DeleteKorisnik")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedKorisnik(string id)
        {
            korisnik korisnik = await db.korisnik.FindAsync(id);
            aspnetuser aspnetuser = await db.aspnetusers.FindAsync(id);

            if (korisnik.putanjaDoProfilneSlike != null)
            {
                string putanjaSlike = Server.MapPath(korisnik.putanjaDoProfilneSlike);

                if (System.IO.File.Exists(putanjaSlike))
                {
                    System.IO.File.Delete(putanjaSlike);
                }
            }

            db.aspnetusers.Remove(aspnetuser);
            db.korisnik.Remove(korisnik);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexKorisnik");
        }
        #endregion

        #region PoduzećeRelated
        //lista poduzeća u bazi
        public async Task<ActionResult> IndexPoduzeće()
        {
            return View("~/Views/Administrator/PoduzećeRelated/IndexPoduzeće.cshtml", await db.poduzeće.ToListAsync());
        }
        //promjeni lozinku poduzeća - get
        public async Task<ActionResult> ChangePasswordPoduzeće(string id)
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
            return View("~/Views/Administrator/PoduzećeRelated/ChangePasswordPoduzeće.cshtml", promijeniLozinku);
        }
        //promjeni lozinku poduzeća - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePasswordPoduzeće([Bind(Include = "ID,StaraLozinka,NovaLozinka,PotvrdiNovuLozinku")] PromijeniLozinku promijeniLozinku)
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
                            return View("~/Views/Administrator/PoduzećeRelated/ChangePasswordPoduzeće.cshtml", promijeniLozinku);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Stara lozinka nije ispravna!");
                        PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                        return View("~/Views/Administrator/PoduzećeRelated/ChangePasswordPoduzeće.cshtml", promijeniLozinku);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lozinka ne može sadržavati samo razmak u sebi ili biti prazna!");
                    PromijeniLozinku _promijeniLozinku = new PromijeniLozinku();
                    return View("~/Views/Administrator/PoduzećeRelated/ChangePasswordPoduzeće.cshtml", promijeniLozinku);
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

                return RedirectToAction("IndexPoduzeće");
            }
            PromijeniLozinku promijeniLozinku_ = new PromijeniLozinku();
            return View("~/Views/Administrator/PoduzećeRelated/ChangePasswordPoduzeće.cshtml", promijeniLozinku_);
        }
        //kreiraj poduzeće - get
        public ActionResult CreatePoduzeće()
        {
            return View("~/Views/Administrator/PoduzećeRelated/CreatePoduzeće.cshtml");
        }
        //kreiraj poduzeće - post
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePoduzeće(RegisterViewModelPoduzeće registerViewModel)
        {
            RegisterViewModelPoduzeće _registerViewModel = new RegisterViewModelPoduzeće { };

            if (ModelState.IsValid)
            {
                var poduzećePostoji = from c in db.aspnetusers where c.UserName.Equals(registerViewModel.Username) select c;

                if (poduzećePostoji.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već postoji poduzeće sa istim korisničkim imenom.");

                    return View("~/Views/Administrator/PoduzećeRelated/CreatePoduzeće.cshtml", _registerViewModel);
                }
          
             
                var user = new ApplicationUser { UserName = registerViewModel.Username, Email = registerViewModel.Username + "@cityinfo.com" };
                var result = await UserManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("Poduzeće"));
                    await UserManager.AddToRoleAsync(user.Id, "Poduzeće");

                    poduzeće poduzeće = new poduzeće { idPoduzeće = user.Id, korisničkoImePoduzeće = registerViewModel.Username, javanPoduzeće = false, imePoduzeće = registerViewModel.ImePoduzeća };
                    db.poduzeće.Add(poduzeće);

                    await db.SaveChangesAsync();

                    return RedirectToAction("IndexPoduzeće");
                }
                else
                {
                    List<string> errori = new List<string>();
                    foreach (string errors in result.Errors)
                    {
                        errori.Add(errors);
                    }

                    foreach (string regerror in errori)
                    {
                        ModelState.AddModelError(string.Empty, regerror);
                    }
                }
                return View("~/Views/Administrator/PoduzećeRelated/CreatePoduzeće.cshtml", _registerViewModel);
            }

            return View("~/Views/Administrator/PoduzećeRelated/CreatePoduzeće.cshtml", _registerViewModel);
        }
        //brisanje poduzeća - get
        public async Task<ActionResult> DeletePoduzeće(string id)
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
            return View("~/Views/Administrator/PoduzećeRelated/DeletePoduzeće.cshtml", poduzeće);
        }
        //brisanje poduzeća - post
        [HttpPost, ActionName("DeletePoduzeće")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedPoduzeće(string id)
        {
            poduzeće poduzeće = await db.poduzeće.FindAsync(id);
            aspnetuser aspnetuser = await db.aspnetusers.FindAsync(id);

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

            db.aspnetusers.Remove(aspnetuser);
            db.poduzeće.Remove(poduzeće);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPoduzeće");
        }
        #endregion

        #region DjelatnostRelated
        //kreiraj djelatnost - get
        public ActionResult CreateDjelatnost()
        {
            return View("~/Views/Administrator/DjelatnostRelated/Create.cshtml");
        }

        //kreiraj djelatnost - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDjelatnost([Bind(Include = "idDjelatnost,imeDjelatnost")] djelatnost djelatnost)
        {
            var rezDjelatnost = db.djelatnost.OrderBy(x => x.idDjelatnost).AsEnumerable().Select(x => x.idDjelatnost);
            int rezIDInt;

            if (ModelState.IsValid)
            {
                if (rezDjelatnost.Count() != 0)
                {
                    var djelatnostMatch = from c in db.djelatnost where c.imeDjelatnost.Equals(djelatnost.imeDjelatnost) select c;

                    if (djelatnostMatch.Count() != 0)
                    {
                        ModelState.AddModelError(string.Empty, "Već ima djelatnost u bazi sa takvim imenom.");
                        return View("~/Views/Administrator/DjelatnostRelated/Create.cshtml", djelatnost);

                    }

                    var rezID = rezDjelatnost.Last();

                    rezIDInt = (rezID);
                    rezIDInt++;
                }
                else
                {
                    rezIDInt = 1;
                }

                djelatnost djelatnostFinal = new djelatnost { idDjelatnost = rezIDInt, imeDjelatnost = djelatnost.imeDjelatnost };
                db.djelatnost.Add(djelatnostFinal);
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }

            return View("~/Views/Administrator/DjelatnostRelated/Create.cshtml", djelatnost);
        }

        //izborni meni biranja koju djelatnost ažurirati
        public ActionResult EditMeniDjelatnost()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idDjelatnost = new SelectList(db.djelatnost, "idDjelatnost", "imeDjelatnost");
            return View("~/Views/Administrator/DjelatnostRelated/EditMeni.cshtml");
        }

        //izborni meni biranja koju djelatnost obrisati
        public ActionResult DeleteMeniDjelatnost()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idDjelatnost = new SelectList(db.djelatnost, "idDjelatnost", "imeDjelatnost");
            return View("~/Views/Administrator/DjelatnostRelated/DeleteMeni.cshtml");
        }

        //djelatnost za ažuriranje - get
        public async Task<ActionResult> EditDjelatnostGet(djelatnost _djelatnost)
        {
            if (_djelatnost.idDjelatnost == null)
            {
                return RedirectToAction("EditMeniDjelatnost");
            }
            djelatnost djelatnost = await db.djelatnost.FindAsync(_djelatnost.idDjelatnost);
            if (djelatnost == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("EditMeniDjelatnost");
            }
            return View("~/Views/Administrator/DjelatnostRelated/Edit.cshtml", djelatnost);
        }

        //djelatnost za ažuriranje - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDjelatnost([Bind(Include = "idDjelatnost,imeDjelatnost")] djelatnost djelatnost)
        {
            if (ModelState.IsValid)
            {
                var djelatnostMatch = from c in db.djelatnost where c.imeDjelatnost.Equals(djelatnost.imeDjelatnost) select c;

                if (djelatnostMatch.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već ima djelatnost u bazi sa takvim imenom.");
                    return View("~/Views/Administrator/DjelatnostRelated/Edit.cshtml", djelatnost);

                }

                db.Entry(djelatnost).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }
            return View("~/Views/Administrator/DjelatnostRelated/Edit.cshtml", djelatnost);
        }

        //djelatnost za brisanje - get
        public async Task<ActionResult> DeleteDjelatnostGet(djelatnost _djelatnost)
        {
            if (_djelatnost.idDjelatnost == null)
            {
                return RedirectToAction("DeleteMeniDjelatnost");
            }
            djelatnost djelatnost = await db.djelatnost.FindAsync(_djelatnost.idDjelatnost);
            if (djelatnost == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("DeleteMeniDjelatnost");
            }
            return View("~/Views/Administrator/DjelatnostRelated/Delete.cshtml", djelatnost);
        }

        //djelatnost za ažuriranje - post
        [HttpPost, ActionName("DeleteDjelatnost")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedDjelatnost(djelatnost _djelatnost)
        {
            djelatnost djelatnost = await db.djelatnost.FindAsync(_djelatnost.idDjelatnost);
            db.djelatnost.Remove(djelatnost);
            await db.SaveChangesAsync();
            return RedirectToAction("GlavniMeni");
        }
        #endregion

        #region GradRelated
        //detekcija promjene županije u dropdown meniju županije
        [HttpGet]
        public JsonResult ŽupanijaChanged(int idŽupanija)
        {
            var data = (from c in db.grad where c.idŽupanija == idŽupanija select c);

            return Json(new SelectList(data.ToArray(), "idGrad", "imeGrad"), JsonRequestBehavior.AllowGet);
        }

        //kreiranje grada - get
        public ActionResult CreateGrad()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");

            return View("~/Views/Administrator/GradRelated/Create.cshtml");
        }

        //kreiranje grada - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGrad([Bind(Include = "idGrad,imeGrad,idŽupanija")] grad grad)
        {
            var županije = from c in db.županija select c;

            if(županije.Count() == 0)
            {
                TempData["status"] = "Match";
                return RedirectToAction("CreateGrad");
            }

            var rezGrad = db.grad.OrderBy(x => x.idGrad).AsEnumerable().Select(x => x.idGrad);
            int rezIDInt;

            if (ModelState.IsValid)
            {
                if (rezGrad.Count() != 0)
                {
                    var gradMatch1 = from c in db.grad where c.imeGrad.Equals(grad.imeGrad) && c.idŽupanija == grad.idŽupanija select c;

                    if (gradMatch1.Count() != 0)
                    {
                        ModelState.AddModelError(string.Empty, "Već ima grad u bazi sa takvim imenom i takvom županijom.");
                        ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
                        return View("~/Views/Administrator/GradRelated/Create.cshtml", grad);

                    }

                    var rezID = rezGrad.Last();

                    rezIDInt = rezID;
                    rezIDInt++;
                }
                else
                {
                    rezIDInt = 1;
                }
                županija županija = db.županija.Find(grad.idŽupanija);

                grad gradFinal = new grad { idGrad = rezIDInt, imeGrad = grad.imeGrad, idŽupanija = županija.idŽupanija };
                db.grad.Add(gradFinal);
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            return View("~/Views/Administrator/GradRelated/Create.cshtml", grad);
        }

        //ažuriranje grada izborni meni 
        public ActionResult EditMeniGrad()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            return View("~/Views/Administrator/GradRelated/EditMeni.cshtml");
        }

        //brisanje grada izborni meni
        public ActionResult DeleteMeniGrad()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            return View("~/Views/Administrator/GradRelated/DeleteMeni.cshtml");
        }

        //ažuriranje grada - get
        public async Task<ActionResult> EditGradGet(grad _grad)
        {
            if (_grad.idGrad == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grad grad = await db.grad.FindAsync(_grad.idGrad);
            if (grad == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("EditMeniGrad");
            }

            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            return View("~/Views/Administrator/GradRelated/Edit.cshtml", grad);
        }

        //ažuriranje grada - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGrad([Bind(Include = "idGrad,imeGrad,idŽupanija")] grad grad)
        {
            if (ModelState.IsValid)
            {
                var gradMatch1 = from c in db.grad where c.imeGrad.Equals(grad.imeGrad) && c.idŽupanija == grad.idŽupanija select c;

                if (gradMatch1.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već ima grad u bazi sa takvim imenom i takvom županijom.");
                    ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
                    return View("~/Views/Administrator/GradRelated/Create.cshtml", grad);

                }

                db.Entry(grad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            return View("~/Views/Administrator/GradRelated/Edit.cshtml", grad);
        }

        //brisanje grada - get
        public async Task<ActionResult> DeleteGradGet(grad _grad)
        {
            if (_grad.idGrad == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            grad grad = await db.grad.FindAsync(_grad.idGrad);
            if (grad == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("DeleteMeniGrad");
            }

            return View("~/Views/Administrator/GradRelated/Delete.cshtml", grad);
        }

        //brisanje grada - post
        [HttpPost, ActionName("DeleteGrad")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedGrad(grad _grad)
        {
            grad grad = await db.grad.FindAsync(_grad.idGrad);
            db.grad.Remove(grad);
            await db.SaveChangesAsync();
            return RedirectToAction("GlavniMeni");
        }
        #endregion

        #region ŽupanijaRelated
        //kreiranje županije - get
        public ActionResult CreateŽupanija()
        {
            return View("~/Views/Administrator/ŽupanijaRelated/Create.cshtml");
        }

        //kreiranje županije - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateŽupanija([Bind(Include = "idŽupanija,imeŽupanija")] županija županija)
        {
            var rezŽupanija = db.županija.OrderBy(x => x.idŽupanija).AsEnumerable().Select(x => x.idŽupanija);
            int rezIDInt;

            if (ModelState.IsValid)
            {
                if (rezŽupanija.Count() != 0)
                {
                    var županijaMatch = from c in db.županija where c.imeŽupanija.Equals(županija.imeŽupanija) select c;

                    if (županijaMatch.Count() != 0)
                    {
                        ModelState.AddModelError(string.Empty, "Već ima županija u bazi sa takvim imenom.");
                        return View("~/Views/Administrator/ŽupanijaRelated/Create.cshtml", županija);

                    }

                    var rezID = rezŽupanija.Last();

                    rezIDInt = (rezID);
                    rezIDInt++;
                }
                else
                {
                    rezIDInt = 1;
                }

                županija županijaFinal = new županija { idŽupanija = rezIDInt, imeŽupanija = županija.imeŽupanija };
                db.županija.Add(županijaFinal);
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }

            return View("~/Views/Administrator/ŽupanijaRelated/Create.cshtml", županija);
        }

        //ažuriranje županije izborni meni
        public ActionResult EditMeniŽupanija()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            return View("~/Views/Administrator/ŽupanijaRelated/EditMeni.cshtml");
        }

        //brisanje županije izborni meni
        public ActionResult DeleteMeniŽupanija()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            return View("~/Views/Administrator/ŽupanijaRelated/DeleteMeni.cshtml");
        }

        //ažuriranje županije - get
        public async Task<ActionResult> EditŽupanijaGet(županija _županija)
        {
            if (_županija.idŽupanija == null)
            {
                return RedirectToAction("EditMeniŽupanija");
            }
            županija županija = await db.županija.FindAsync(_županija.idŽupanija);
            if (županija == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("EditMeniŽupanija");
            }
            return View("~/Views/Administrator/ŽupanijaRelated/Edit.cshtml", županija);
        }

        //ažuriranje županije - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditŽupanija([Bind(Include = "idŽupanija,imeŽupanija")] županija županija)
        {
            if (ModelState.IsValid)
            {
                var županijaMatch = from c in db.županija where c.imeŽupanija.Equals(županija.imeŽupanija) select c;

                if (županijaMatch.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već ima županija u bazi sa takvim imenom.");
                    return View("~/Views/Administrator/ŽupanijaRelated/Edit.cshtml", županija);

                }

                db.Entry(županija).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }
            return View("~/Views/Administrator/ŽupanijaRelated/Edit.cshtml", županija);
        }

        //brisanje županije - get
        public async Task<ActionResult> DeleteŽupanijaGet(županija _županija)
        {
            if (_županija.idŽupanija == null)
            {
                return RedirectToAction("DeleteMeniŽupanija");
            }
            županija županija = await db.županija.FindAsync(_županija.idŽupanija);
            if (županija == null)
            {
                if (županija == null)
                {
                    TempData["status"] = "Match";
                    return RedirectToAction("DeleteMeniŽupanija");
                }
            }
            return View("~/Views/Administrator/ŽupanijaRelated/Delete.cshtml", županija);
        }

        //brisanje županije - post
        [HttpPost, ActionName("DeleteŽupanija")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedŽupanija(županija _županija)
        {
            županija županija = await db.županija.FindAsync(_županija.idŽupanija);
            db.županija.Remove(županija);
            await db.SaveChangesAsync();
            return RedirectToAction("GlavniMeni");
        }
        #endregion

        #region UlicaRelated
        //detektiranje promjene grada u dropdown listu gradova
        [HttpGet]
        public JsonResult GradChanged(int idGrad)
        {
            var data = (from c in db.ulica where c.idGrad == idGrad select c);

            return Json(new SelectList(data.ToArray(), "idUlica", "imeUlica"), JsonRequestBehavior.AllowGet);
        }

        //kreiranje ulice - get
        public ActionResult CreateUlica()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            return View("~/Views/Administrator/UlicaRelated/Create.cshtml");
        }

        //kreiranje ulice - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUlica([Bind(Include = "idUlica,imeUlica,idGrad")] ulica ulica)
        {
            var županije = from c in db.županija select c;
            var gradovi = from c in db.grad select c;

            if (županije.Count() == 0 || gradovi.Count() == 0)
            {
                TempData["status"] = "Ne možete kreirati ulicu jer još nemate i županiju i grad u bazi!";
                return RedirectToAction("CreateUlica");
            }
           
            var rezUlica = db.ulica.OrderBy(x => x.idUlica).AsEnumerable().Select(x => x.idUlica);
            int rezIDInt;

            if (ModelState.IsValid)
            {
                if (rezUlica.Count() != 0)
                {
                    var ulicaMatch1 = from c in db.ulica where c.imeUlica.Equals(ulica.imeUlica) && c.idGrad == ulica.idGrad select c;

                    if (ulicaMatch1.Count() != 0)
                    {
                        ModelState.AddModelError(string.Empty, "Već ima ulica u bazi sa takvim imenom i takvim gradom.");
                        ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
                        ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
                        return View("~/Views/Administrator/UlicaRelated/Create.cshtml", ulica);

                    }
                   

                    var rezID = rezUlica.Last();

                    rezIDInt = rezID;
                    rezIDInt++;
                }
                else
                {
                    rezIDInt = 1;
                }
                grad grad = db.grad.Find(ulica.idGrad);
                ulica ulicaFinal = new ulica { idUlica = rezIDInt, imeUlica = ulica.imeUlica, idGrad = grad.idGrad };
                db.ulica.Add(ulicaFinal);
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            return View("~/Views/Administrator/UlicaRelated/Create.cshtml", ulica);
        }

        //izborni meni ažuriranja ulica
        public ActionResult EditMeniUlica()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            ViewBag.idUlica = new SelectList(db.ulica, "idUlica", "imeUlica");
            return View("~/Views/Administrator/UlicaRelated/EditMeni.cshtml");
        }

        //izborni meni brisanja ulica
        public ActionResult DeleteMeniUlica()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            ViewBag.idUlica = new SelectList(db.ulica, "idUlica", "imeUlica");
            return View("~/Views/Administrator/UlicaRelated/DeleteMeni.cshtml");
        }

        //ažuriranje ulice - get
        public async Task<ActionResult> EditUlicaGet(ulica _ulica)
        {
            if (_ulica.idUlica == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ulica ulica = await db.ulica.FindAsync(_ulica.idUlica);
            if (ulica == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("EditMeniUlica");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            return View("~/Views/Administrator/UlicaRelated/Edit.cshtml", ulica);
        }

        //ažuriranje ulice - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUlica([Bind(Include = "idUlica,imeUlica,idGrad")] ulica ulica)
        {
            if (ModelState.IsValid)
            {
                var ulicaMatch1 = from c in db.ulica where c.imeUlica.Equals(ulica.imeUlica) && c.idGrad == ulica.idGrad select c;

                if (ulicaMatch1.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Već ima ulica u bazi sa takvim imenom i takvim gradom.");
                    ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
                    ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
                    return View("~/Views/Administrator/UlicaRelated/Create.cshtml", ulica);

                }

                db.Entry(ulica).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("GlavniMeni");
            }
            ViewBag.idŽupanija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");
            ViewBag.idGrad = new SelectList(db.grad, "idGrad", "imeGrad");
            return View("~/Views/Administrator/UlicaRelated/Edit.cshtml", ulica);
        }

        //brisanje ulice - get
        public async Task<ActionResult> DeleteUlicaGet(ulica _ulica)
        {
            if (_ulica.idUlica == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ulica ulica = await db.ulica.FindAsync(_ulica.idUlica);
            if (ulica == null)
            {
                TempData["status"] = "Match";
                return RedirectToAction("DeleteMeniUlica");
            }
            return View("~/Views/Administrator/UlicaRelated/Delete.cshtml", ulica);
        }

        //brisanje ulice - post
        [HttpPost, ActionName("DeleteUlica")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedUlica(ulica _ulica)
        {
            ulica ulica = await db.ulica.FindAsync(_ulica.idUlica);
            db.ulica.Remove(ulica);
            await db.SaveChangesAsync();
            return RedirectToAction("GlavniMeni");
        }
        #endregion

        #region RecenzijaRelated
        //lista recenzija za odobravanje
        public async Task<ActionResult> OdobriRecenzijaList()
        {
            var recenzije = await (from c in db.recenzija where c.odobrenoRecenzija == false select c).ToListAsync();

            return View("~/Views/Administrator/RecenzijaRelated/Index.cshtml", recenzije);
        }
        //pojedina recenzija za odobravanje - get
        public ActionResult OdobriRecenziju(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recenzija recenzija = db.recenzija.Find(int.Parse(id));
            if (recenzija == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View("~/Views/Administrator/RecenzijaRelated/OdobriRecenziju.cshtml", recenzija);
        }
        //pojedina recenzija za odobravanje - post
        public async Task<ActionResult> OdobriRecenzijuConfirmed(string id)
        {
            recenzija recenzija = db.recenzija.Find(int.Parse(id));
            recenzija.odobrenoRecenzija = true;
            await db.SaveChangesAsync();
            return RedirectToAction("OdobriRecenzijaList");
        }
        //pojedina recenzija za odobravanje, brisanje - post
        public async Task<ActionResult> DeleteRecenzija(string id)
        {
            recenzija recenzija = db.recenzija.Find(int.Parse(id));
            db.recenzija.Remove(recenzija);
            await db.SaveChangesAsync();
            return RedirectToAction("OdobriRecenzijaList");
        }
        #endregion

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
