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
    public class IsEmriYokIsParcasiController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriYokIsParcasi
        public ActionResult Index()
        {
            var isEmriYokIsParcasi = db.IsEmriYokIsParcasi.Include(i => i.IsEmri).Include(i => i.YokIsParcasi);
            return View(isEmriYokIsParcasi.ToList());
        }


        // GET: IsEmriYokIsParcasi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriYokIsParcasi isEmriYokIsParcasi = db.IsEmriYokIsParcasi.Find(id);
            if (isEmriYokIsParcasi == null)
            {
                return HttpNotFound();
            }
            return View(isEmriYokIsParcasi);
        }

        // GET: IsEmriYokIsParcasi/Create
        public ActionResult Create(int? IsEmriId)
        {
            if(IsEmriId!=null)
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo",IsEmriId.ToString());
            else
            {
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            }
            ViewBag.YokIsParcasiId = new SelectList(db.YokIsParcasi, "Id", "YokIsParcasiAdi");
            return View();
        }

        // POST: IsEmriYokIsParcasi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsEmriId,YokIsParcasiId")] IsEmriYokIsParcasi isEmriYokIsParcasi)
        {
            if (ModelState.IsValid)
            {
                db.IsEmriYokIsParcasi.Add(isEmriYokIsParcasi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriYokIsParcasi.IsEmriId);
            ViewBag.YokIsParcasiId = new SelectList(db.YokIsParcasi, "Id", "YokIsParcasiAdi", isEmriYokIsParcasi.YokIsParcasiId);
            return View(isEmriYokIsParcasi);
        }

        // GET: IsEmriYokIsParcasi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriYokIsParcasi isEmriYokIsParcasi = db.IsEmriYokIsParcasi.Find(id);
            if (isEmriYokIsParcasi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriYokIsParcasi.IsEmriId);
            ViewBag.YokIsParcasiId = new SelectList(db.YokIsParcasi, "Id", "YokIsParcasiAdi", isEmriYokIsParcasi.YokIsParcasiId);
            return View(isEmriYokIsParcasi);
        }

        // POST: IsEmriYokIsParcasi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsEmriId,YokIsParcasiId")] IsEmriYokIsParcasi isEmriYokIsParcasi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isEmriYokIsParcasi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriYokIsParcasi.IsEmriId);
            ViewBag.YokIsParcasiId = new SelectList(db.YokIsParcasi, "Id", "YokIsParcasiAdi", isEmriYokIsParcasi.YokIsParcasiId);
            return View(isEmriYokIsParcasi);
        }

        // GET: IsEmriYokIsParcasi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriYokIsParcasi isEmriYokIsParcasi = db.IsEmriYokIsParcasi.Find(id);
            if (isEmriYokIsParcasi == null)
            {
                return HttpNotFound();
            }
            return View(isEmriYokIsParcasi);
        }

        // POST: IsEmriYokIsParcasi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriYokIsParcasi isEmriYokIsParcasi = db.IsEmriYokIsParcasi.Find(id);
            db.IsEmriYokIsParcasi.Remove(isEmriYokIsParcasi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: IsEmriYokIsParcasi/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]

        


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
