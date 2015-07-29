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
    public class BaskiEbatlarisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: BaskiEbatlaris
        public ActionResult Index()
        {
            var baskiEbatlari = db.BaskiEbatlari.Include(b => b.BaskiMakineleri).Where(x => x.Silindi == false).OrderBy(x=>x.Boyut);
            return View(baskiEbatlari.ToList());
        }

        // GET: BaskiEbatlaris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaskiEbatlari baskiEbatlari = db.BaskiEbatlari.Find(id);
            if (baskiEbatlari == null)
            {
                return HttpNotFound();
            }
            return View(baskiEbatlari);
        }

        // GET: BaskiEbatlaris/Create
        
        /*
        public ActionResult Create(int IsEmriId)
        {
            Session["IsEmriId"] = IsEmriId;
            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi");
            return View();
        }
        */

        public ActionResult Create()
        {
            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi");
            return View();
        }


        // POST: BaskiEbatlaris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BaskiEbatlariId,Boyut,BaskiMakineleriId,BirimFiyat")] BaskiEbatlari baskiEbatlari)
        {
            if (ModelState.IsValid)
            {
                
                db.BaskiEbatlari.Add(baskiEbatlari);
                db.SaveChanges();
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = int.Parse(Session["IsEmriId"].ToString()), index = 1 }).Success("Yeni baskı boyutu bilgisi ekleme işlemi başarılı");
            }

            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi", baskiEbatlari.BaskiMakineleriId);
            return View(baskiEbatlari);
        }

        // GET: BaskiEbatlaris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaskiEbatlari baskiEbatlari = db.BaskiEbatlari.Find(id);
            if (baskiEbatlari == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi", baskiEbatlari.BaskiMakineleriId);
            return View(baskiEbatlari);
        }

        // POST: BaskiEbatlaris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BaskiEbatlariId,Boyut,BaskiMakineleriId,BirimFiyat")] BaskiEbatlari baskiEbatlari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baskiEbatlari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi", baskiEbatlari.BaskiMakineleriId);
            return View(baskiEbatlari);
        }

        // GET: BaskiEbatlaris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaskiEbatlari baskiEbatlari = db.BaskiEbatlari.Find(id);
            if (baskiEbatlari == null)
            {
                return HttpNotFound();
            }
            return View(baskiEbatlari);
        }
        // POST: BaskiEbatlaris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]public ActionResult DeleteConfirmed(int id)
        {
            BaskiEbatlari baskiEbatlari = db.BaskiEbatlari.Find(id);
            baskiEbatlari.Silindi = true;
            //db.BaskiEbatlari.Remove(baskiEbatlari);
            db.SaveChanges();
            return RedirectToAction("Index").Success("Baskı boyutu silme başarılı");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult _PartialCreate()
        {
            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi");
            return View();
        }
    }
}
