using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;

namespace KendoMatbaa.Controllers
{
    public class IsEmriFilmGiderlerisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriFilmGiderleris
        public ActionResult Index()
        {
            var isEmriFilmGiderleri = db.IsEmriFilmGiderleri.Include(i => i.AltGruplar).Include(i => i.IsEmri);
            return View(isEmriFilmGiderleri.ToList());
        }
        public ActionResult _BelirliIsEmrininFilmGiderleri(int IsEmriId)
        {
            var isEmriFilmGiderleri = db.IsEmriFilmGiderleri.Include(i => i.AltGruplar).Include(i => i.IsEmri).Where(i => i.IsEmriId == IsEmriId);
             return View(isEmriFilmGiderleri.ToList());
        }


        // GET: IsEmriFilmGiderleris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriFilmGiderleri isEmriFilmGiderleri = db.IsEmriFilmGiderleri.Find(id);
            if (isEmriFilmGiderleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriFilmGiderleri);
        }

        // GET: IsEmriFilmGiderleris/Create
        public ActionResult Create(string SeciliIsEmriId)
        {
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 49), "AltGruplarId", "AltGrupAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo",SeciliIsEmriId);
            return View();
        }

        // POST: IsEmriFilmGiderleris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriFilmGiderleriId,IsEmriId,IcFilm,Kapakfilm,AltGruplarId,Silindi")] IsEmriFilmGiderleri isEmriFilmGiderleri)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.IsEmriFilmGiderleri.Add(isEmriFilmGiderleri);
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriFilmGiderleri.IsEmriId, index = 3 }).Success("Film bilgisi ekleme işlemi başarılı");
                }

                ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 49), "AltGruplarId", "AltGrupAdi", isEmriFilmGiderleri.AltGruplarId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriFilmGiderleri.IsEmriId);
                return View(isEmriFilmGiderleri);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriFilmGiderleri.IsEmriId }).Warning("Yetkiniz yok!");
            }

        }


        [HttpPost]
        public ActionResult FilmYok(int IsEmriId)
        {
            int isEmriFilmGiderleriId = FindIsEmriFilmGiderleriIdByIsEmriId(IsEmriId);
            if (isEmriFilmGiderleriId == 0)//demekki  isEmriFilmGiderleri  tablosuna bu iş emri için işlenmiş bir film bilgisi yok
            {
                using (matbaaEntities ent = new matbaaEntities())
                {
                    IsEmriFilmGiderleri o = new IsEmriFilmGiderleri();
                    o.IsEmriId = IsEmriId;
                    o.IcFilm = 0;
                    o.Kapakfilm = 0;
                    o.AltGruplarId = 1327;
                    o.Silindi = false;
                    db.IsEmriFilmGiderleri.Add(o);
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriId, index = 3 }).Success("İş Emri için film kullanılmadığı bilgisi  eklendi");
                }
            }
            //IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Find(isEmriKullanilanLaminastyonlarId);
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriId, index = 3 }).Success("İş Emri için film kullanılmadığı bilgisi  eklendi");
        }


        // GET: IsEmriFilmGiderleris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriFilmGiderleri isEmriFilmGiderleri = db.IsEmriFilmGiderleri.Find(id);
            if (isEmriFilmGiderleri == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 49), "AltGruplarId", "AltGrupAdi", isEmriFilmGiderleri.AltGruplarId);
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriFilmGiderleri.IsEmriId);
            return View(isEmriFilmGiderleri);
        }

        // POST: IsEmriFilmGiderleris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriFilmGiderleriId,IsEmriId,IcFilm,Kapakfilm,AltGruplarId,Silindi")] IsEmriFilmGiderleri isEmriFilmGiderleri)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(isEmriFilmGiderleri).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index", "IsEmri").Success("Film bilgisi güncelleme işlemi başarılı");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriFilmGiderleri.IsEmriId, index = 3 }).Success("Film bilgisi güncelleme işlemi başarılı");
                }
                ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 49), "AltGruplarId", "AltGrupAdi", isEmriFilmGiderleri.AltGruplarId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriFilmGiderleri.IsEmriId);
                return View(isEmriFilmGiderleri);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriFilmGiderleri.IsEmriId}).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriFilmGiderleris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriFilmGiderleri isEmriFilmGiderleri = db.IsEmriFilmGiderleri.Find(id);
            if (isEmriFilmGiderleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriFilmGiderleri);
        }

        // POST: IsEmriFilmGiderleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriFilmGiderleri isEmriFilmGiderleri = db.IsEmriFilmGiderleri.Find(id);
            db.IsEmriFilmGiderleri.Remove(isEmriFilmGiderleri);
            db.SaveChanges();
            //return RedirectToAction("Index", "IsEmri").Success("Film bilgisi silme işlemi başarılı");
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriFilmGiderleri.IsEmriId, index = 3 }).Success("Film bilgisi silme işlemi başarılı");
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
