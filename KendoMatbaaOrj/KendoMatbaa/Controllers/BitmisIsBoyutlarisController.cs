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
    public class BitmisIsBoyutlarisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: BitmisIsBoyutlaris
        public ActionResult Index()
        {
            return View(db.BitmisIsBoyutlari.Where(x=>x.Silindi==false).ToList());
        }

        // GET: BitmisIsBoyutlaris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitmisIsBoyutlari bitmisIsBoyutlari = db.BitmisIsBoyutlari.Find(id);
            if (bitmisIsBoyutlari == null)
            {
                return HttpNotFound();
            }
            return View(bitmisIsBoyutlari);
        }

        // GET: BitmisIsBoyutlaris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BitmisIsBoyutlaris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BitmisIsBoyutuAdi,Silindi")] BitmisIsBoyutlari bitmisIsBoyutlari)
        {
            if (ModelState.IsValid)
            {
                db.BitmisIsBoyutlari.Add(bitmisIsBoyutlari);
                db.SaveChanges();
                return RedirectToAction("TabView","IsEmri",new { IsEmriId = Convert.ToInt32(Session["BitmisIsBoyutunaGidecekIsEmriId"].ToString()), index = 1 }).Success("Yeni bitmiş iş boyutu başarı ile eklendi..");
                //return Redirect("Index");
            }

            return View(bitmisIsBoyutlari);
        }

        // GET: BitmisIsBoyutlaris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitmisIsBoyutlari bitmisIsBoyutlari = db.BitmisIsBoyutlari.Find(id);
            if (bitmisIsBoyutlari == null)
            {
                return HttpNotFound();
            }
            return View(bitmisIsBoyutlari);
        }

        // POST: BitmisIsBoyutlaris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BitmisIsBoyutuAdi,Silindi")] BitmisIsBoyutlari bitmisIsBoyutlari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitmisIsBoyutlari).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(bitmisIsBoyutlari);
        }

        // GET: BitmisIsBoyutlaris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitmisIsBoyutlari bitmisIsBoyutlari = db.BitmisIsBoyutlari.Find(id);
            if (bitmisIsBoyutlari == null)
            {
                return HttpNotFound();
            }
            return View(bitmisIsBoyutlari);
        }

        // POST: BitmisIsBoyutlaris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //BitmisIsBoyutlari bitmisIsBoyutlari = db.BitmisIsBoyutlari.Find(id);
            //db.BitmisIsBoyutlari.Remove(bitmisIsBoyutlari);
            //db.SaveChanges();
            using (matbaaEntities ent = new matbaaEntities())
            {
                var bu = ent.BitmisIsBoyutlari.Find(id);
                if (bu != null)
                {
                    bu.Silindi = true;
                    ent.SaveChanges();
                }
            }
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
