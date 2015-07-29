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
    public class IsEmriKalipMalzemeGiderlerisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriKalipMalzemeGiderleris
        public ActionResult Index()
        {
            var isEmriKalipMalzemeGiderleri = db.IsEmriKalipMalzemeGiderleri.Include(i => i.AltGruplar).Include(i => i.IsEmri);
            return View(isEmriKalipMalzemeGiderleri.ToList());
        }

        public ActionResult _BelirliIsEmrininKalipMalzemeGiderleri(int IsEmriId)
        {
            var isEmriKalipMalzemeGiderleri = db.IsEmriKalipMalzemeGiderleri.Include(i => i.AltGruplar).Include(i => i.IsEmri).Where(i => i.IsEmriId == IsEmriId);
            return View(isEmriKalipMalzemeGiderleri.ToList());
        }


        // GET: IsEmriKalipMalzemeGiderleris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
            if (isEmriKalipMalzemeGiderleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKalipMalzemeGiderleri);
        }

        // GET: IsEmriKalipMalzemeGiderleris/Create
        public ActionResult Create(string SeciliIsEmriId)
        {
            ViewBag.SeciliIsEmriId = SeciliIsEmriId;ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId==47), "AltGruplarId", "AltGrupAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo",SeciliIsEmriId);
            return View();
        }

        // POST: IsEmriKalipMalzemeGiderleris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriId,IsEmriKalipMalzemeGiderleriId,Ic,Dis,AltGruplarId,KullanilanKalipSayisi,Silindi")] IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {

                if (ModelState.IsValid)
                {
                    db.IsEmriKalipMalzemeGiderleri.Add(isEmriKalipMalzemeGiderleri);
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKalipMalzemeGiderleri.IsEmriId, index = 5 }).Success("Kalıp malzeme giderleri bilgisi ekleme işlemi başarılı");
                }

                ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 47), "AltGruplarId", "AltGrupAdi", isEmriKalipMalzemeGiderleri.AltGruplarId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKalipMalzemeGiderleri.IsEmriId);
                return View(isEmriKalipMalzemeGiderleri);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKalipMalzemeGiderleri.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriKalipMalzemeGiderleris/Edit/5
        public ActionResult Edit(int? id)
        {
            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
            if (isEmriKalipMalzemeGiderleri == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", isEmriKalipMalzemeGiderleri.AltGruplarId);
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKalipMalzemeGiderleri.IsEmriId);
            return View(isEmriKalipMalzemeGiderleri);
             */
            if (id > 100000000)
            {
                //id yerine indexten IsEmriId bilgisi geliyor..bunu IsEmriKullanilacakKagitId bilgisine cevirmek gerekli
                //Once IsEmriKalipMalzemeGiderleri Tablsoıunda o işemrine ait kayıt var mı bak baklım
                if (IsEmriKalipMalzemeGiderBilgileriKaydiVarmi(id.Value))
                {

                    id = FindIsEmriKalipMalzemeGideriByIsEmriId(id.Value);
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    IsEmriKalipMalzemeGiderleri isemrikalipmalzemegiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
                    if (isemrikalipmalzemegiderleri == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isemrikalipmalzemegiderleri.IsEmriId);
                    ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 47), "AltGruplarId", "AltGrupAdi", isemrikalipmalzemegiderleri.AltGruplarId);
                    return View(isemrikalipmalzemegiderleri);
                }
                else
                {
                    //İş Emrine ait KAlip Malzeme bilgisi bulunamadı
                    return RedirectToAction("Index", "IsEmri").Warning("İş Emrine ait kalıp malzeme giderleri bilgisi bulunamadı");
                }
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IsEmriKalipMalzemeGiderleri isemrikalipmalzemegiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
                if (isemrikalipmalzemegiderleri == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isemrikalipmalzemegiderleri.IsEmriId);
                ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 47), "AltGruplarId", "AltGrupAdi", isemrikalipmalzemegiderleri.AltGruplarId);
                return View(isemrikalipmalzemegiderleri);
            }
        }

        // POST: IsEmriKalipMalzemeGiderleris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriId,IsEmriKalipMalzemeGiderleriId,Ic,Dis,AltGruplarId,KullanilanKalipSayisi,Silindi")] IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {

                if (ModelState.IsValid)
                {
                    db.Entry(isEmriKalipMalzemeGiderleri).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index", "IsEmri").Success("Kalıp malzeme giderleri bilgisi güncelleme işlemi başarılı");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKalipMalzemeGiderleri.IsEmriId, index = 5 }).Success("Kalıp malzeme giderleri bilgisi güncelleme işlemi başarılı");
                }
                ViewBag.AltGruplarId = new SelectList(db.AltGruplar.Where(AltGruplar => AltGruplar.StokGruplariId == 47), "AltGruplarId", "AltGrupAdi", isEmriKalipMalzemeGiderleri.AltGruplarId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriKalipMalzemeGiderleri.IsEmriId);
                return View(isEmriKalipMalzemeGiderleri);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKalipMalzemeGiderleri.IsEmriId }).Warning("Yetkiniz yok!");
            }

        }

        // GET: IsEmriKalipMalzemeGiderleris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
            if (isEmriKalipMalzemeGiderleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriKalipMalzemeGiderleri);
        }

        // POST: IsEmriKalipMalzemeGiderleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriKalipMalzemeGiderleri isEmriKalipMalzemeGiderleri = db.IsEmriKalipMalzemeGiderleri.Find(id);
            db.IsEmriKalipMalzemeGiderleri.Remove(isEmriKalipMalzemeGiderleri);
            db.SaveChanges();
            //return RedirectToAction("Index", "IsEmri").Success("Kalıp malzeme giderleri bilgisi silme işlemi başarılı");
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriKalipMalzemeGiderleri.IsEmriId, index = 5 }).Success("Kalıp malzeme giderleri bilgisi silme işlemi başarılı");

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
