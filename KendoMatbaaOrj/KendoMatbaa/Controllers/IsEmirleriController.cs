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
    public class IsEmirleriController : BaseController{
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmirleri
        public ActionResult Index()
        {
            var isEmri = db.IsEmri.Include(i => i.Birim).Include(i => i.IKBSBirim);
            return View(isEmri.ToList());
        }

        // GET: IsEmirleri/Details/5
        public ActionResult Details(int? id){
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isEmri = db.IsEmri.Find(id);
            if (isEmri == null)
            {
                return HttpNotFound();
            }
            return View(isEmri);
        }

        // GET: IsEmirleri/Create
        public ActionResult Create()
        {
            ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi");
            ViewBag.BitmisIsBoyutlariId = new SelectList(db.BitmisIsBoyutlari, "Id", "BitmisIsBoyutuAdi");
            ViewBag.IKBSBirimUN = new SelectList(db.IKBSBirim, "BirimUN", "Adi");
            return View();
        }

        // POST: IsEmirleri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriId,IsNo,TarihSayi,Adi,Personel,Telefon,KabulTarihi,TeslimTarihi,Aciklama,BirimId,Silindi,IlgiliEPosta,IKBSBirimUN")] IsEmri isEmri)
        {
            if (ModelState.IsValid)
            {
                db.IsEmri.Add(isEmri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi", isEmri.BirimId);
            
            ViewBag.IKBSBirimUN = new SelectList(db.IKBSBirim, "BirimUN", "Adi", isEmri.IKBSBirimUN);
            return View(isEmri);
        }

        // GET: IsEmirleri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isEmri = db.IsEmri.Find(id);
            if (isEmri == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi", isEmri.BirimId);
          
            ViewBag.IKBSBirimUN = new SelectList(db.IKBSBirim, "BirimUN", "Adi", isEmri.IKBSBirimUN);
            return View(isEmri);
        }

        // POST: IsEmirleri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriId,IsNo,TarihSayi,Adi,Personel,Telefon,KabulTarihi,TeslimTarihi,Aciklama,BirimId,Silindi,IlgiliEPosta,IKBSBirimUN")] IsEmri isEmri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isEmri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirimId = new SelectList(db.Birim, "BirimId", "BirimAdi", isEmri.BirimId);
            
            ViewBag.IKBSBirimUN = new SelectList(db.IKBSBirim, "BirimUN", "Adi", isEmri.IKBSBirimUN);
            return View(isEmri);
        }

        // GET: IsEmirleri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmri isEmri = db.IsEmri.Find(id);
            if (isEmri == null)
            {
                return HttpNotFound();
            }
            return View(isEmri);
        }

        // POST: IsEmirleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmri isEmri = db.IsEmri.Find(id);
            db.IsEmri.Remove(isEmri);
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
