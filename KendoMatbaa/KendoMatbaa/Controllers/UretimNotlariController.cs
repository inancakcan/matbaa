﻿using System;
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
    public class UretimNotlariController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /UretimNotlari/
        public ActionResult Index()
        {
            var uretimnotlari = db.UretimNotlari.Include(u => u.IsEmri);
            return View(uretimnotlari.ToList());
        }

        // GET: /UretimNotlari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimNotlari uretimnotlari = db.UretimNotlari.Find(id);
            if (uretimnotlari == null)
            {
                return HttpNotFound();
            }
            return View(uretimnotlari);
        }

        // GET: /UretimNotlari/Create
        public ActionResult Create()
        {
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            return View();
        }

        // POST: /UretimNotlari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UretimNotlariId,TasarimaOnayVeren,BaskiOncesiHazirlikNotu,BaskiNotu,MucellithaneNotu,IsKesimiProgramNo,PaketBilgileri,IsEmriId")] UretimNotlari uretimnotlari)
        {
            if (ModelState.IsValid)
            {
                db.UretimNotlari.Add(uretimnotlari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretimnotlari.IsEmriId);
            return View(uretimnotlari);
        }

        // GET: /UretimNotlari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimNotlari uretimnotlari = db.UretimNotlari.Find(id);
            if (uretimnotlari == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretimnotlari.IsEmriId);
            return View(uretimnotlari);
        }

        // POST: /UretimNotlari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UretimNotlariId,TasarimaOnayVeren,BaskiOncesiHazirlikNotu,BaskiNotu,MucellithaneNotu,IsKesimiProgramNo,PaketBilgileri,IsEmriId")] UretimNotlari uretimnotlari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uretimnotlari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", uretimnotlari.IsEmriId);
            return View(uretimnotlari);
        }

        // GET: /UretimNotlari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UretimNotlari uretimnotlari = db.UretimNotlari.Find(id);
            if (uretimnotlari == null)
            {
                return HttpNotFound();
            }
            return View(uretimnotlari);
        }

        // POST: /UretimNotlari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UretimNotlari uretimnotlari = db.UretimNotlari.Find(id);
            db.UretimNotlari.Remove(uretimnotlari);
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
