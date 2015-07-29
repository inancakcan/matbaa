using System;using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;

namespace KendoMatbaa.Controllers
{
    public class YokIsParcasiController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: YokIsParcasi
        public ActionResult Index()
        {
            return View(db.YokIsParcasi.ToList());
        }

        // GET: YokIsParcasi/Details/5
        public ActionResult Details(int? id)
        {if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YokIsParcasi yokIsParcasi = db.YokIsParcasi.Find(id);
            if (yokIsParcasi == null)
            {
                return HttpNotFound();
            }
            return View(yokIsParcasi);
        }

        // GET: YokIsParcasi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YokIsParcasi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,YokIsParcasiAdi")] YokIsParcasi yokIsParcasi)
        {
            if (ModelState.IsValid)
            {
                db.YokIsParcasi.Add(yokIsParcasi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yokIsParcasi);
        }

        // GET: YokIsParcasi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YokIsParcasi yokIsParcasi = db.YokIsParcasi.Find(id);
            if (yokIsParcasi == null)
            {
                return HttpNotFound();
            }
            return View(yokIsParcasi);
        }

        // POST: YokIsParcasi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,YokIsParcasiAdi")] YokIsParcasi yokIsParcasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yokIsParcasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yokIsParcasi);
        }

        // GET: YokIsParcasi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YokIsParcasi yokIsParcasi = db.YokIsParcasi.Find(id);
            if (yokIsParcasi == null)
            {
                return HttpNotFound();
            }
            return View(yokIsParcasi);
        }

        // POST: YokIsParcasi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YokIsParcasi yokIsParcasi = db.YokIsParcasi.Find(id);
            db.YokIsParcasi.Remove(yokIsParcasi);
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
