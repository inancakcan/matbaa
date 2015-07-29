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
    public class BaskiMakinelerisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: BaskiMakineleris
        public ActionResult Index()
        {
            return View(db.BaskiMakineleri.ToList().Where(x => x.Silindi == false).OrderBy(x => x.MakineAdi));
        }

        // GET: BaskiMakineleris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaskiMakineleri baskiMakineleri = db.BaskiMakineleri.Find(id);
            if (baskiMakineleri == null)
            {
                return HttpNotFound();
            }
            return View(baskiMakineleri);
        }

        // GET: BaskiMakineleris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BaskiMakineleris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BaskiMakineleriId,MakineAdi")] BaskiMakineleri baskiMakineleri)
        {
            if (ModelState.IsValid)
            {
                db.BaskiMakineleri.Add(baskiMakineleri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baskiMakineleri);
        }

        // GET: BaskiMakineleris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaskiMakineleri baskiMakineleri = db.BaskiMakineleri.Find(id);
            if (baskiMakineleri == null)
            {
                return HttpNotFound();
            }
            return View(baskiMakineleri);
        }

        // POST: BaskiMakineleris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BaskiMakineleriId,MakineAdi")] BaskiMakineleri baskiMakineleri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baskiMakineleri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baskiMakineleri);
        }

        // GET: BaskiMakineleris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaskiMakineleri baskiMakineleri = db.BaskiMakineleri.Find(id);
            if (baskiMakineleri == null)
            {
                return HttpNotFound();
            }
            return View(baskiMakineleri);
        }

        // POST: BaskiMakineleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaskiMakineleri baskiMakineleri = db.BaskiMakineleri.Find(id);
            baskiMakineleri.Silindi = true;
            //db.BaskiMakineleri.Remove(baskiMakineleri);
            db.SaveChanges();
            return RedirectToAction("Index").Success("Baskı makinesi silme başarılı");
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
