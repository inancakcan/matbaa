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
    public class IsEmriKullanilanLaminasyonlarsController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriKullanilanLaminasyonlars
        public ActionResult Index()
        {
            var isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Include(i => i.AltGruplar).Include(i => i.IsEmri);
            return View(isEmriKullanilanLaminasyonlar.ToList());
        }


        public ActionResult _BelirliIsEmrininLaminasyonlari(int IsEmriId)
        {
            var isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Include(i => i.AltGruplar).Include(i => i.IsEmri).Where(i => i.IsEmriId == IsEmriId); 
            return View(isEmriKullanilanLaminasyonlar.ToList());
        }

        // GET: IsEmriKullanilanLaminasyonlars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Find(id);
            if (isEmriKullanilanLaminasyonlar == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKullanilanLaminasyonlar);
        }

        // GET: IsEmriKullanilanLaminasyonlars/Create
        public ActionResult Create(string SeciliIsEmriId)
        {
            string IsEmriId =  Request["IsEmriId"];
            ViewBag.LaminasyonlarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId==48), "AltGruplarId", "AltGrupAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", SeciliIsEmriId);
            return View();
        }

        // POST: IsEmriKullanilanLaminasyonlars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KullanilanLaminasyonlarId,IsEmriId,LaminasyonlarId")] IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar)
        {

            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.IsEmriKullanilanLaminasyonlar.Add(isEmriKullanilanLaminasyonlar);
                    db.SaveChanges();
                    //return RedirectToAction("TabView", "IsEmri", new { id = isEmriKullanilanLaminasyonlar.IsEmriId, index =2}).Success("Laminasyon bilgisi ekleme işlemi başarılı");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilanLaminasyonlar.IsEmriId, index = 2 }).Success("Laminasyon bilgisi ekleme işlemi başarılı");
                }

                ViewBag.LaminasyonlarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 48), "AltGruplarId", "AltGrupAdi", isEmriKullanilanLaminasyonlar.LaminasyonlarId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKullanilanLaminasyonlar.IsEmriId);
                return View(isEmriKullanilanLaminasyonlar);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilanLaminasyonlar.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }


        [HttpPost]
        public ActionResult LaminasyonYok(int IsEmriId)
        {
            int isEmriKullanilanLaminastyonlarId = FindIsEmriKullanilanLaminasyonlarIdByIsEmriId(IsEmriId);
            if (isEmriKullanilanLaminastyonlarId == 0)//demekki  IsEmriKullanilanLaminasyonlar tablosuna bu iş emri için işlenmiş bir laminasyon bilgisi yok
            {
                using (matbaaEntities ent = new matbaaEntities())
                {
                    IsEmriKullanilanLaminasyonlar o = new IsEmriKullanilanLaminasyonlar();
                    o.IsEmriId = IsEmriId;
                    o.LaminasyonlarId = 1323;
                    db.IsEmriKullanilanLaminasyonlar.Add(o);
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriId, index = 2 }).Success("İş Emri için laminasyon kullanılmadığı bilgisi  eklendi");
                }
            }
            //IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Find(isEmriKullanilanLaminastyonlarId);
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = IsEmriId, index = 2 }).Success("İş Emri için laminasyon kullanılmadığı bilgisi  eklendi");
        }


        

        // GET: IsEmriKullanilanLaminasyonlars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Find(id);
            if (isEmriKullanilanLaminasyonlar == null)
            {
                return HttpNotFound();
            }
            ViewBag.LaminasyonlarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 48), "AltGruplarId", "AltGrupAdi", isEmriKullanilanLaminasyonlar.LaminasyonlarId);
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKullanilanLaminasyonlar.IsEmriId);
            return View(isEmriKullanilanLaminasyonlar);
        }

        // POST: IsEmriKullanilanLaminasyonlars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KullanilanLaminasyonlarId,IsEmriId,LaminasyonlarId")] IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(isEmriKullanilanLaminasyonlar).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index", "IsEmri").Success("Laminasyon bilgisi güncelleme işlemi başarılı");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilanLaminasyonlar.IsEmriId, index = 2 }).Success("Laminasyon bilgisi güncelleme işlemi başarılı");
                }
                ViewBag.LaminasyonlarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 48), "AltGruplarId", "AltGrupAdi", isEmriKullanilanLaminasyonlar.LaminasyonlarId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKullanilanLaminasyonlar.IsEmriId);
                return View(isEmriKullanilanLaminasyonlar);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilanLaminasyonlar.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriKullanilanLaminasyonlars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Find(id);
            if (isEmriKullanilanLaminasyonlar == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKullanilanLaminasyonlar);
        }

        // POST: IsEmriKullanilanLaminasyonlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriKullanilanLaminasyonlar isEmriKullanilanLaminasyonlar = db.IsEmriKullanilanLaminasyonlar.Find(id);
            db.IsEmriKullanilanLaminasyonlar.Remove(isEmriKullanilanLaminasyonlar);
            db.SaveChanges();
            //return RedirectToAction("Index", "IsEmri").Success("Laminasyon bilgisi silme işlemi başarılı");
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKullanilanLaminasyonlar.IsEmriId, index = 2 }).Success("Laminasyon bilgisi silme işlemi başarılı");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult MyPartialView()
        {
            return PartialView("MyPartialView");
        }

        //public ActionResult _DevamEtmekteOlanIsler()
        //{
        //    var oDevamEdenIslerResult = db.spDevamEdenIsler();
        //    return View(oDevamEdenIslerResult.ToList());
        //}
    }
}
