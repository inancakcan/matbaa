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
    public class AltUretimAsamalariController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /AltUretimAsamalari/
        public ActionResult Index()
        {
            return View(db.AltUretimAsamalari.ToList());
        }

        // GET: /AltUretimAsamalari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltUretimAsamalari alturetimasamalari = db.AltUretimAsamalari.Find(id);
            if (alturetimasamalari == null)
            {
                return HttpNotFound();
            }
            return View(alturetimasamalari);
        }

        // GET: /AltUretimAsamalari/Create
        public ActionResult Create()
        {
            List<SelectListItem> items = UretimAsamalariDoldur();
            ViewBag.Months = items;
            return View();
        }

        protected List<SelectListItem> UretimAsamalariDoldur()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var oUretimAsamalari = db.UretimAsamalari.ToList();
            items.Add(new SelectListItem { Text = "--Üretim aşamasını seçiniz--", Value = "0" });
            foreach (var item in oUretimAsamalari)
            {
                items.Add(new SelectListItem { Text = item.UretimAsamasiAdi, Value = item.UretimAsamalariId.ToString() });
            }
            return items;
        }

        // POST: /AltUretimAsamalari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AltUretimAsamalariId,AltUretimAsamalariAdi,UretimAsamalariId")] AltUretimAsamalari alturetimasamalari)
        {



            if (ModelState.IsValid)
            {
                db.AltUretimAsamalari.Add(alturetimasamalari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alturetimasamalari);
        }

        // GET: /AltUretimAsamalari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltUretimAsamalari alturetimasamalari = db.AltUretimAsamalari.Find(id);
            if (alturetimasamalari == null)
            {
                return HttpNotFound();
            }
            return View(alturetimasamalari);
        }

        // POST: /AltUretimAsamalari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AltUretimAsamalariId,AltUretimAsamalariAdi,UretimAsamalariId")] AltUretimAsamalari alturetimasamalari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alturetimasamalari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alturetimasamalari);
        }

        // GET: /AltUretimAsamalari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltUretimAsamalari alturetimasamalari = db.AltUretimAsamalari.Find(id);
            if (alturetimasamalari == null)
            {
                return HttpNotFound();
            }
            return View(alturetimasamalari);
        }

        // POST: /AltUretimAsamalari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AltUretimAsamalari alturetimasamalari = db.AltUretimAsamalari.Find(id);
            db.AltUretimAsamalari.Remove(alturetimasamalari);
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
