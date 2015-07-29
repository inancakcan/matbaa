using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.App_Start;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class StokHareketsController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: StokHarekets
        public ActionResult Index()
        {
            var stokHareket = db.StokHareket.Include(s => s.AltGruplar).OrderByDescending(s=>s.StokHareketId).Take(200);
            return View(stokHareket.ToList());
        }

        // GET: StokHarekets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokHareket stokHareket = db.StokHareket.Find(id);
            if (stokHareket == null)
            {
                return HttpNotFound();
            }
            return View(stokHareket);
        }

        // GET: StokHarekets/Create
        public ActionResult Create(int? SeciliAltGruplarId)
        {
            if (SeciliAltGruplarId != null)
            {
                ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", SeciliAltGruplarId);
            }
            else
            {
                ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi");    
            }
            
            //ViewBag.ArtiEksi=new SelectList(new[] { "-", "+"}
            //    .Select(x => new { value = x, text = x }),
            //    "value", "text", "15");
            return View();
        }

       

        // POST: StokHarekets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StokHareketId,AltGruplarId,Miktar,Tarih,Hareket,Personel,Yon,Silindi,IsEmriId")] StokHareket stokHareket)
        {
            if (ModelState.IsValid)
            {
                db.StokHareket.Add(stokHareket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokHareket.AltGruplarId);
            return View(stokHareket);
        }

        //public ActionResult CreateSpecific(int AltGruplarId)
        //{
        //    ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", AltGruplarId);
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateSpecific([Bind(Include = "StokHareketId,AltGruplarId,Miktar,Tarih,Hareket,Personel,Yon,Silindi,IsEmriId")] StokHareket stokHareket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.StokHareket.Add(stokHareket);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokHareket.AltGruplarId);
        //    return View(stokHareket);
        //}
        // GET: StokHarekets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokHareket stokHareket = db.StokHareket.Find(id);
            if (stokHareket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokHareket.AltGruplarId);
            return View(stokHareket);
        }

        // POST: StokHarekets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StokHareketId,AltGruplarId,Miktar,Tarih,Hareket,Personel,Yon,Silindi,IsEmriId")] StokHareket stokHareket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stokHareket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index").Success("Stok hareketi başarıyla güncellendi...");
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokHareket.AltGruplarId);
            return View(stokHareket);
        }

        // GET: StokHarekets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokHareket stokHareket = db.StokHareket.Find(id);
            if (stokHareket == null)
            {
                return HttpNotFound();
            }
            return View(stokHareket);
        }

        // POST: StokHarekets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StokHareket stokHareket = db.StokHareket.Find(id);
            db.StokHareket.Remove(stokHareket);
            db.SaveChanges();
            return RedirectToAction("Index");
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
