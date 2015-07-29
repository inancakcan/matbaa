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
    public class IsCinsiController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsCinsi
        public ActionResult Index()
        {
            //return View(db.IsCinsi.ToList());
            return View(db.IsCinsi.Where(i=>i.Silindi==false));
        }

        // GET: IsCinsi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsCinsi isCinsi = db.IsCinsi.Find(id);
            if (isCinsi == null)
            {
                return HttpNotFound();
            }
            return View(isCinsi);
        }

        // GET: IsCinsi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IsCinsi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsCinsiId,CinsAdi,Silindi")] IsCinsi isCinsi)
        {
            if (ModelState.IsValid)
            {
                db.IsCinsi.Add(isCinsi);
                db.SaveChanges();
                return RedirectToAction("Index", "IsCinsi").Success("İş cinsi kaydı başarı ile oluşturuldu...");
            }

            return View(isCinsi);
        }

        // GET: IsCinsi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsCinsi isCinsi = db.IsCinsi.Find(id);
            if (isCinsi == null)
            {
                return HttpNotFound();
            }
            return View(isCinsi);
        }

        // POST: IsCinsi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsCinsiId,CinsAdi,Silindi")] IsCinsi isCinsi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isCinsi).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "IsCinsi").Success("İş cinsi kaydı başarı ile güncellendi...");
            }
            return View(isCinsi);
        }

        // GET: IsCinsi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsCinsi isCinsi = db.IsCinsi.Find(id);
            if (isCinsi == null)
            {
                return HttpNotFound();
            }
            return View(isCinsi);
        }

        public bool IsCinsiSil(int IsCinsiId)
        {
            using (matbaaEntities ent = new matbaaEntities())
            {
                bool Basarim = false;
                try
                {
                    ent.spUpdateIsCinsiSilindi(IsCinsiId, true);
                    Basarim = true;
                }
                catch (Exception exc)
                {
                    Basarim = false;
                }
                return Basarim;
            }
        }


        // POST: IsCinsi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //IsCinsi isCinsi = db.IsCinsi.Find(id);
            //db.IsCinsi.Remove(isCinsi);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            if (IsCinsiSil(id))
            {
                return RedirectToAction("Index", "IsCinsi").Success("İş cinsi kaydı başarı ile silindi...");
            }
            else
            {
                return RedirectToAction("Index", "IsCinsi").Warning("İş cinsi kaydı silinemedi!");
            }

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
