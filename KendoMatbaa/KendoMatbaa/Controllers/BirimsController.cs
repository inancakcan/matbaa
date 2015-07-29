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
    public class BirimsController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: Birims
        public ActionResult Index()
        {
            return View(db.Birim.ToList());
        }

        // GET: Birims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Birim birim = db.Birim.Find(id);
            if (birim == null)
            {
                return HttpNotFound();
            }
            return View(birim);
        }

        // GET: Birims/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Birims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BirimId,BirimAdi,BirimKodu")] Birim birim)
        {
            if (ModelState.IsValid)
            {
                db.Birim.Add(birim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(birim);
        }

        // GET: Birims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Birim birim = db.Birim.Find(id);
            if (birim == null)
            {
                return HttpNotFound();
            }
            return View(birim);
        }

        // POST: Birims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BirimId,BirimAdi,BirimKodu")] Birim birim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(birim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(birim);
        }

        // GET: Birims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Birim birim = db.Birim.Find(id);
            if (birim == null)
            {
                return HttpNotFound();
            }
            return View(birim);
        }

        // POST: Birims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Birim birim = db.Birim.Find(id);
            db.Birim.Remove(birim);
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
