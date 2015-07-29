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
    public class RollersController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: Rollers
        public ActionResult Index()
        {
            var roller = db.Roller.Include(r => r.Personel);
            return View(roller.ToList());
        }

        // GET: Rollers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roller roller = db.Roller.Find(id);
            if (roller == null)
            {
                return HttpNotFound();
            }
            return View(roller);
        }

        // GET: Rollers/Create
        public ActionResult Create()
        {
            ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi");
            return View();
        }

        // POST: Rollers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RollerId,RolAdi,PersonelUN,RolAciklama")] Roller roller)
        {
            if (ModelState.IsValid)
            {
                db.Roller.Add(roller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", roller.PersonelUN);
            return View(roller);
        }

        // GET: Rollers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roller roller = db.Roller.Find(id);
            if (roller == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", roller.PersonelUN);
            return View(roller);
        }

        // POST: Rollers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RollerId,RolAdi,PersonelUN,RolAciklama")] Roller roller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonelUN = new SelectList(db.Personel, "PersonelUN", "PersonelAdiSoyadi", roller.PersonelUN);
            return View(roller);
        }

        // GET: Rollers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roller roller = db.Roller.Find(id);
            if (roller == null)
            {
                return HttpNotFound();
            }
            return View(roller);
        }

        // POST: Rollers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roller roller = db.Roller.Find(id);
            db.Roller.Remove(roller);
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
