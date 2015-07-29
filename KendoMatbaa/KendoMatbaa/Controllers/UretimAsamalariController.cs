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
    public class UretimAsamalariController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /UretimAsamalari/
        public ActionResult Index()
        {
            return View(db.UretimAsamalari.ToList());
        }

        // GET: /UretimAsamalari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimAsamalari uretimasamalari = db.UretimAsamalari.Find(id);
            if (uretimasamalari == null)
            {
                return HttpNotFound();
            }
            return View(uretimasamalari);
        }

        // GET: /UretimAsamalari/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UretimAsamalari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UretimAsamalariId,UretimAsamasiAdi")] UretimAsamalari uretimasamalari)
        {
            if (ModelState.IsValid)
            {
                db.UretimAsamalari.Add(uretimasamalari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uretimasamalari);
        }

        // GET: /UretimAsamalari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimAsamalari uretimasamalari = db.UretimAsamalari.Find(id);
            if (uretimasamalari == null)
            {
                return HttpNotFound();
            }
            return View(uretimasamalari);
        }

        // POST: /UretimAsamalari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UretimAsamalariId,UretimAsamasiAdi")] UretimAsamalari uretimasamalari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uretimasamalari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uretimasamalari);
        }

        // GET: /UretimAsamalari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimAsamalari uretimasamalari = db.UretimAsamalari.Find(id);
            if (uretimasamalari == null)
            {
                return HttpNotFound();
            }
            return View(uretimasamalari);
        }

        // POST: /UretimAsamalari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UretimAsamalari uretimasamalari = db.UretimAsamalari.Find(id);
            db.UretimAsamalari.Remove(uretimasamalari);
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
