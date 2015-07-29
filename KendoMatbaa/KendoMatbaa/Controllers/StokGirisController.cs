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
    public class StokGirisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: StokGiris
        public ActionResult Index()
        {
            var stokGiris = db.StokGiris.Include(s => s.AltGruplar).Include(s => s.FizikselBirim).Include(s => s.Personel).OrderByDescending(s=>s.StokGirisId);
            return View(stokGiris.ToList());
        }

        // GET: StokGiris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokGiris stokGiris = db.StokGiris.Find(id);
            if (stokGiris == null)
            {
                return HttpNotFound();
            }
            return View(stokGiris);
        }

        // GET: StokGiris/Create
        public ActionResult Create()
        {
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi");
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik");
            ViewBag.StokGirisiniYapan = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi");
            return View();
        }

        // POST: StokGiris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StokGirisId,Tarih,AltGruplarId,FizikselBirimId,AlindigiFirmaAdi,FaturaTarihi,FaturaNo,BirimFiyat,Miktar,StokGirisiniYapan")] StokGiris stokGiris)
        {
            if (ModelState.IsValid)
            {
                db.StokGiris.Add(stokGiris);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokGiris.AltGruplarId);
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik", stokGiris.FizikselBirimId);
            ViewBag.StokGirisiniYapan = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", stokGiris.StokGirisiniYapan);
            return View(stokGiris);
        }

        // GET: StokGiris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokGiris stokGiris = db.StokGiris.Find(id);
            if (stokGiris == null)
            {
                return HttpNotFound();
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokGiris.AltGruplarId);
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik", stokGiris.FizikselBirimId);
            ViewBag.StokGirisiniYapan = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", stokGiris.StokGirisiniYapan);
            return View(stokGiris);
        }

        // POST: StokGiris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StokGirisId,Tarih,AltGruplarId,FizikselBirimId,AlindigiFirmaAdi,FaturaTarihi,FaturaNo,BirimFiyat,Miktar,StokGirisiniYapan")] StokGiris stokGiris)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stokGiris).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AltGruplarId = new SelectList(db.AltGruplar, "AltGruplarId", "AltGrupAdi", stokGiris.AltGruplarId);
            ViewBag.FizikselBirimId = new SelectList(db.FizikselBirim, "FizikselBirimId", "Nicelik", stokGiris.FizikselBirimId);
            ViewBag.StokGirisiniYapan = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", stokGiris.StokGirisiniYapan);
            return View(stokGiris);
        }

        // GET: StokGiris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StokGiris stokGiris = db.StokGiris.Find(id);
            if (stokGiris == null)
            {
                return HttpNotFound();
            }
            return View(stokGiris);
        }

        // POST: StokGiris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StokGiris stokGiris = db.StokGiris.Find(id);
            db.StokGiris.Remove(stokGiris);
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
