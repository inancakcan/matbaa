using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class AltGruplarsController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: AltGruplars
        public ActionResult Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                var altGruplar =db.AltGruplar.Include(a => a.FizikselBirim).Include(a => a.StokGruplari).OrderBy(s => s.AltGrupAdi);
                return View(altGruplar.ToList());
            }

            else
            {
                //var altGruplar = db.AltGruplar.Include(a => a.FizikselBirim).Include(a => a.StokGruplari).OrderBy(s => s.AltGrupAdi);
                var altGruplar = db.AltGruplar.Include(a => a.FizikselBirim).Include(a => a.StokGruplari).Where(s=>s.AltGrupAdi.Contains(searchString.ToUpper())).OrderBy(s => s.AltGrupAdi);
                //altGruplar = altGruplar.Where(s => s.AltGrupAdi.ToString().Contains(searchString.ToUpper()));
                //altGruplar = altGruplar.Where(s => s.Silindi == false); return View(isEmri.ToList());
                return View(altGruplar.ToList());}
        }

        // GET: AltGruplars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltGruplar altGruplar = db.AltGruplar.Find(id);
            if (altGruplar == null)
            {
                return HttpNotFound();
            }
            return View(altGruplar);
        }

        // GET: AltGruplars/Create
        public ActionResult Create()
        {
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik");
            ViewBag.StokGruplariId = new SelectList(db.StokGruplari, "StokGruplariId", "StokGrupAdi");
            return View();
        }

        // POST: AltGruplars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AltGruplarId,StokGruplariId,AltGrupAdi,KritikSeviye,Otokopi,BirimFiyat,SarfMi,FizikselBirimId")] AltGruplar altGruplar)
        {
            if (ModelState.IsValid)
            {
                db.AltGruplar.Add(altGruplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik", altGruplar.FizikselBirimId);
            ViewBag.StokGruplariId = new SelectList(db.StokGruplari, "StokGruplariId", "StokGrupAdi", altGruplar.StokGruplariId);
            return View(altGruplar);
        }

        // GET: AltGruplars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltGruplar altGruplar = db.AltGruplar.Find(id);
            if (altGruplar == null)
            {
                return HttpNotFound();
            }
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik", altGruplar.FizikselBirimId);
            ViewBag.StokGruplariId = new SelectList(db.StokGruplari, "StokGruplariId", "StokGrupAdi", altGruplar.StokGruplariId);
            return View(altGruplar);
        }

        // POST: AltGruplars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AltGruplarId,StokGruplariId,AltGrupAdi,KritikSeviye,Otokopi,BirimFiyat,SarfMi,FizikselBirimId")] AltGruplar altGruplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altGruplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik", altGruplar.FizikselBirimId);
            ViewBag.StokGruplariId = new SelectList(db.StokGruplari, "StokGruplariId", "StokGrupAdi", altGruplar.StokGruplariId);
            return View(altGruplar);
        }

        // GET: AltGruplars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltGruplar altGruplar = db.AltGruplar.Find(id);
            if (altGruplar == null)
            {
                return HttpNotFound();
            }
            return View(altGruplar);
        }

        // POST: AltGruplars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AltGruplar altGruplar = db.AltGruplar.Find(id);
            db.AltGruplar.Remove(altGruplar);
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
