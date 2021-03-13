using CityInformation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using iText.Layout.Element;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using System.IO;

namespace CityInformation.Controllers
{
    public class HomeController : Controller
    {
        private ModelsContext db = new ModelsContext();

        //naslovna stranica
        public ActionResult Index()
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            return View();
        }

        //detektiranje promjene dropdown izbornika županije
        [HttpGet]
        public JsonResult ŽupanijaChanged(int idŽupanija)
        {
            var data = (from c in db.grad where c.idŽupanija == idŽupanija select c);

            return Json(new SelectList(data.ToArray(), "idGrad", "imeGrad"), JsonRequestBehavior.AllowGet);
        }

        //detektiranje promjene dropdown izbornika grada
        [HttpGet]
        public JsonResult GradChanged(int idGrad)
        {
            var data = (from c in db.ulica where c.idGrad == idGrad select c);

            return Json(new SelectList(data.ToArray(), "idUlica", "imeUlica"), JsonRequestBehavior.AllowGet);
        }

        //profil za goste korisnike - get
        public ActionResult ProfilGosti(string id)
        {
            if (TempData["status"] != null)
            {
                ViewBag.Message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            poduzeće poduzeće = db.poduzeće.Find(id);
            IEnumerable<slika> modelSlika = (from c in db.slika where c.idPoduzeća.Equals(poduzeće.idPoduzeće) select c).ToList();
            IEnumerable<recenzija> recenzije = (from c in db.recenzija
                                                where c.poduzećeRecenzijaID.Equals(poduzeće.idPoduzeće)
                         && c.odobrenoRecenzija == true
                                                select c).ToList();
            recenzija recenzija = new recenzija
            {
                poduzećeRecenzijaID = poduzeće.idPoduzeće,
                vlasnikRecenzijaID = User.Identity.GetUserId(),
                datumRecenzija = DateTime.Now.ToShortDateString() + ";" + DateTime.Now.ToShortTimeString()
            };

            Tuple<IEnumerable<slika>, poduzeće, IEnumerable<recenzija>, recenzija> tuple = Tuple.Create(modelSlika, poduzeće, recenzije, recenzija);

            return View("~/Views/Poduzeće/ProfilGosti.cshtml", tuple);
        }

        //kreiranje recenzije i zapisivanje u bazu - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KreirajRecenziju([Bind(Include = "tekstRecenzija,poduzećeRecenzijaID,vlasnikRecenzijaID,datumRecenzija")] recenzija recenzija)
        {
            if(User.Identity.IsAuthenticated == false)
            {
                TempData["status"] = "Morate biti registrirani korisnik da možete objavljivati recenzije.";
                return RedirectToAction("ProfilGosti", new { id = recenzija.poduzećeRecenzijaID });
            }
            else  if (User.IsInRole("Korisnik") == false)
            {
                TempData["status"] = "Morate biti ulogirani kao običan korisnik da bi mogli kreirati recenzije na druga poduzeća.";
                return RedirectToAction("ProfilGosti", new { id = recenzija.poduzećeRecenzijaID });
            }

            var rezRecenzija = db.recenzija.OrderBy(x => x.idRecenzija).AsEnumerable().Select(x => x.idRecenzija);
            int rezIDInt;

            if (ModelState.IsValid)
            {
                if (rezRecenzija.Count() != 0)
                {
                    var rezID = rezRecenzija.Last();

                    rezIDInt = rezID;
                    rezIDInt++;
                }
                else
                {
                    rezIDInt = 1;
                }
                recenzija _recenzija = new recenzija
                {
                    datumRecenzija = DateTime.Now.ToShortDateString(),
                    poduzećeRecenzijaID = recenzija.poduzećeRecenzijaID,
                    tekstRecenzija = recenzija.tekstRecenzija,
                    odobrenoRecenzija = false,
                    vlasnikRecenzijaID = User.Identity.GetUserId(),
                    idRecenzija = rezIDInt
                };
                db.recenzija.Add(_recenzija);
                await db.SaveChangesAsync();
                TempData["status"] = "Recenzija je uspješno poslana administratoru na odobrenje.";
                return RedirectToAction("ProfilGosti", new { id = recenzija.poduzećeRecenzijaID });
            }
            poduzeće poduzeće = db.poduzeće.Find(recenzija.poduzećeRecenzijaID);
            IEnumerable<slika> modelSlika = (from c in db.slika where c.idPoduzeća.Equals(poduzeće.idPoduzeće) select c).ToList();
            IEnumerable<recenzija> recenzije = (from c in db.recenzija
                                                where c.poduzećeRecenzijaID.Equals(poduzeće.idPoduzeće)
                         && c.odobrenoRecenzija == true
                                                select c).ToList();
            recenzija __recenzija = new recenzija
            {
                poduzećeRecenzijaID = poduzeće.idPoduzeće,
                vlasnikRecenzijaID = User.Identity.GetUserId(),
                datumRecenzija = DateTime.Now.ToShortDateString()
            };
            Tuple<IEnumerable<slika>, poduzeće, IEnumerable<recenzija>, recenzija> tuple = Tuple.Create(modelSlika, poduzeće, recenzije, __recenzija);

            return View("~/Views/Poduzeće/ProfilGosti.cshtml", tuple);
        }

        //view sa filterom i listom poduzeća - get
        [HttpGet]
        public ActionResult Pretraživanje()
        {
            Pretraživanje pretraživanje = new Pretraživanje();
            var poduzeće = (from c in db.poduzeće where c.javanPoduzeće == true select c).ToList();

            ViewBag.grad = new SelectList(db.grad, "idGrad", "imeGrad");
            ViewBag.djelatnost = new SelectList(db.djelatnost, "idDjelatnost", "imeDjelatnost");
            ViewBag.ulica = new SelectList(db.ulica, "idUlica", "imeUlica");
            ViewBag.županija = new SelectList(db.županija, "idŽupanija", "imeŽupanija");

            Tuple<List<poduzeće>, Pretraživanje> tuple = Tuple.Create(poduzeće, pretraživanje);

            return View(tuple);
        }

        //vraćanje rezultata filtera i ažuriranje liste poduzeća - post
        [HttpPost]
        public async Task<ActionResult> PretraživanjeRezultata([Bind(Include = "tekst,djelatnost,grad,ulica,županija")] Pretraživanje rez)
        {
            if (rez.tekst != null)
            {
                List<poduzeće> poduzeće = new List<poduzeće>();

                poduzeće = await (from c in db.poduzeće
                                  where c.imePoduzeće.Contains(rez.tekst) ||
                        c.tipPoslovnogObjekta.ToString().Contains(rez.tekst) || c.gradPoduzeće.ToString().Contains(rez.tekst)
                        || c.županijaPoduzeće.ToString().Contains(rez.tekst) || c.ulicaPoduzeće.ToString().Contains(rez.tekst) && c.javanPoduzeće == true
                                  select c).ToListAsync();

                return PartialView("~/Views/Home/ListaPoduzeća.cshtml", poduzeće);
            }
            else
            {
                List<poduzeće> poduzeće = new List<poduzeće>();

                if (rez.djelatnost != null && rez.grad != null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                where c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.gradPoduzeće.ToString().Equals(rez.grad) &&
          c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće where c.javanPoduzeće == true select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                where c.gradPoduzeće.ToString().Equals(rez.grad) &&
          c.županijaPoduzeće.ToString().Equals(rez.županija) && c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.županijaPoduzeće.ToString().Equals(rez.županija) &&
                c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad != null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
                c.gradPoduzeće.ToString().Equals(rez.grad) && c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad != null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.županijaPoduzeće.ToString().Equals(rez.županija) &&
                c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.gradPoduzeće.ToString().Equals(rez.grad) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.županijaPoduzeće.ToString().Equals(rez.županija) &&
                c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                            where c.gradPoduzeće.ToString().Equals(rez.grad) &&
                c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.gradPoduzeće.ToString().Equals(rez.grad) &&
                c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
                c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
                c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad != null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                            where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
                c.gradPoduzeće.ToString().Equals(rez.grad) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.gradPoduzeće.ToString().Equals(rez.grad) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = await (from c in db.poduzeće
                                      where c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                      select c).ToListAsync();
                }
                return PartialView("~/Views/Home/ListaPoduzeća.cshtml", poduzeće);
            }
        }

        //metoda za pomoć pri kreiranju pdf-a, služi za detektciju sličnu kao metoda PretraživanjeRezultata
        private List<poduzeće> PretragaRasporedaOdvoza(Pretraživanje rez)
        {
            if (rez.tekst != null)
            {
                List<poduzeće> poduzeće = new List<poduzeće>();

                poduzeće = (from c in db.poduzeće
                                 where c.imePoduzeće.Contains(rez.tekst) ||
                       c.tipPoslovnogObjekta.ToString().Contains(rez.tekst) || c.gradPoduzeće.ToString().Contains(rez.tekst)
                       || c.županijaPoduzeće.ToString().Contains(rez.tekst) || c.ulicaPoduzeće.ToString().Contains(rez.tekst) && c.javanPoduzeće == true
                                 select c).ToList();

                return poduzeće;
            }
            else
            {
                List<poduzeće> poduzeće = new List<poduzeće>();

                if (rez.djelatnost != null && rez.grad != null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.gradPoduzeće.ToString().Equals(rez.grad) &&
               c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće where c.javanPoduzeće == true select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.gradPoduzeće.ToString().Equals(rez.grad) &&
               c.županijaPoduzeće.ToString().Equals(rez.županija) && c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.županijaPoduzeće.ToString().Equals(rez.županija) &&
               c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad != null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
               c.gradPoduzeće.ToString().Equals(rez.grad) && c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad != null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.županijaPoduzeće.ToString().Equals(rez.županija) &&
               c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.gradPoduzeće.ToString().Equals(rez.grad) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija != null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.županijaPoduzeće.ToString().Equals(rez.županija) &&
               c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.gradPoduzeće.ToString().Equals(rez.grad) &&
         c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.gradPoduzeće.ToString().Equals(rez.grad) &&
               c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
               c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
               c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad != null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) &&
         c.gradPoduzeće.ToString().Equals(rez.grad) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost != null && rez.grad == null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.tipPoslovnogObjekta.ToString().Equals(rez.djelatnost) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad != null && rez.županija == null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.gradPoduzeće.ToString().Equals(rez.grad) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija != null && rez.ulica == null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.županijaPoduzeće.ToString().Equals(rez.županija) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                else if (rez.djelatnost == null && rez.grad == null && rez.županija == null && rez.ulica != null)
                {
                    poduzeće = (from c in db.poduzeće
                                     where c.ulicaPoduzeće.ToString().Equals(rez.ulica) && c.javanPoduzeće == true
                                     select c).ToList();
                }
                return poduzeće;
            }
        }

        //pomoćna metoda za kreiranje pdf-a
        private static Table CreateTable(List<poduzeće> poduzeća)
        {
            List<poduzeće> poduzeće = poduzeća;

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD, "Cp1250", true);
            PdfFont regular = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN, "Cp1250", true);

            Table table = new Table(new float[5]).UseAllAvailableWidth();


            Cell cell1 = new Cell().Add(new Paragraph("Ime poduzeća"));
            cell1.SetBorder(new SolidBorder(2));
            cell1.SetBackgroundColor(new DeviceRgb(52, 219, 235));
            cell1.SetFont(bold);
            table.AddCell(cell1);
            Cell cell2 = new Cell().Add(new Paragraph("Županija"));
            cell2.SetBorder(new SolidBorder(2));
            cell2.SetBackgroundColor(new DeviceRgb(52, 219, 235));
            cell2.SetFont(bold);
            table.AddCell(cell2);
            Cell cell3 = new Cell().Add(new Paragraph("Grad"));
            cell3.SetBorder(new SolidBorder(2));
            cell3.SetBackgroundColor(new DeviceRgb(52, 219, 235));
            cell3.SetFont(bold);
            table.AddCell(cell3);
            Cell cell11 = new Cell().Add(new Paragraph("Ulica"));
            cell11.SetBorder(new SolidBorder(2));
            cell11.SetBackgroundColor(new DeviceRgb(52, 219, 235));
            cell11.SetFont(bold);
            table.AddCell(cell11);
            Cell cell5 = new Cell().Add(new Paragraph("Tip objekta"));
            cell5.SetBorder(new SolidBorder(2));
            cell5.SetBackgroundColor(new DeviceRgb(52, 219, 235));
            cell5.SetFont(bold);
            table.AddCell(cell5);
            foreach (poduzeće item in poduzeće)
            {
                Cell cell6 = new Cell().Add(new Paragraph(item.imePoduzeće));
                cell6.SetBorder(new SolidBorder(2));
                cell6.SetBackgroundColor(new DeviceRgb(52, 219, 235));
                cell6.SetFont(regular);
                table.AddCell(cell6);
                Cell cell7 = new Cell().Add(new Paragraph(item.županija.imeŽupanija));
                cell7.SetBorder(new SolidBorder(2));
                cell7.SetBackgroundColor(new DeviceRgb(52, 219, 235));
                cell7.SetFont(regular);
                table.AddCell(cell7);
                Cell cell8 = new Cell().Add(new Paragraph(item.grad.imeGrad));
                cell8.SetBorder(new SolidBorder(2));
                cell8.SetBackgroundColor(new DeviceRgb(52, 219, 235));
                cell8.SetFont(regular);
                table.AddCell(cell8);
              
                Cell cell12 = new Cell().Add(new Paragraph(item.ulica.imeUlica));
                cell12.SetBorder(new SolidBorder(2));
                cell12.SetBackgroundColor(new DeviceRgb(52, 219, 235));
                cell12.SetFont(regular);
                table.AddCell(cell12);
                Cell cell10 = new Cell().Add(new Paragraph(item.djelatnost.imeDjelatnost));
                cell10.SetBorder(new SolidBorder(2));
                cell10.SetBackgroundColor(new DeviceRgb(52, 219, 235));
                cell10.SetFont(regular);
                table.AddCell(cell10);
            }

            return table;
        }

        //kreiranje pdf-a i ispis
        [HttpPost]
        public void CreatePDF(Pretraživanje rezultat)
        {
            Random r = new Random();
            int i = r.Next(1, 100000000);
            string rnd = i.ToString();

            var exportFolder = Server.MapPath("~/PDF");
            var exportFile = Path.Combine(exportFolder, rnd + ".pdf");

            var writer = new PdfWriter(exportFile);
            var pdf = new PdfDocument(writer);
            var doc = new Document(pdf);

            Table table = new Table(new float[10]).UseAllAvailableWidth();
            Cell cell = new Cell(1, 10).Add(new Paragraph("Kreirano: " + DateTime.Now.ToString()));
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetPadding(5);
            cell.SetBackgroundColor(new DeviceRgb(7, 140, 242));
            table.AddCell(cell);

            Table table3 = new Table(new float[10]).UseAllAvailableWidth();
            Cell cell4 = new Cell(1, 10).SetBorder(Border.NO_BORDER).Add(new Paragraph(""));
            cell4.SetPadding(5);
            table3.AddCell(cell4);

            Table table2 = new Table(new float[10]).UseAllAvailableWidth();
            Cell cell3 = new Cell(1, 10).Add(new Paragraph("Popis poduzeca koja odgovaraju vašem pretraživanju"));
            cell3.SetTextAlignment(TextAlignment.CENTER);
            cell3.SetPadding(5);
            cell3.SetBackgroundColor(new DeviceRgb(7, 140, 242));
            table2.AddCell(cell3);

            Table table4 = new Table(new float[10]).UseAllAvailableWidth();
            table4.SetVerticalAlignment(VerticalAlignment.BOTTOM);
            Cell cell5 = new Cell(1, 10).Add(new Paragraph("City info"));
            cell5.SetTextAlignment(TextAlignment.CENTER);
            cell5.SetPadding(5);
            cell5.SetBackgroundColor(new DeviceRgb(64, 180, 222));
            table4.AddCell(cell5);

            doc.Add(table4);
            doc.Add(table);
            doc.Add(table3);
            doc.Add(table2);

            doc.Add(CreateTable(PretragaRasporedaOdvoza(rezultat)));

            doc.Close();

            Response.ContentType = "application/pdf";
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + rnd + ".pdf");
            Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Response.TransmitFile(Server.MapPath("~/PDF/" + rnd + ".pdf"));
            Response.End();

            if (System.IO.File.Exists(Server.MapPath("~/PDF/" + rnd + ".pdf")))
            {
                try
                {
                    System.IO.File.Delete(Server.MapPath("~/PDF/" + rnd + ".pdf"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}