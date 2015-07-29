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
    public class MucellithaneProgramNoPaketBilgileriController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: /MucellithaneProgramNoPaketBilgileri/
        public ActionResult Index()
        {
            var mucellithaneprogramnopaketbilgileri = db.MucellithaneProgramNoPaketBilgileri.Include(m => m.IsEmri);
            return View(mucellithaneprogramnopaketbilgileri.ToList());
        }

        // GET: /MucellithaneProgramNoPaketBilgileri/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MucellithaneProgramNoPaketBilgileri mucellithaneprogramnopaketbilgileri = db.MucellithaneProgramNoPaketBilgileri.Find(id);
            if (mucellithaneprogramnopaketbilgileri == null)
            {
                return HttpNotFound();
            }
            return View(mucellithaneprogramnopaketbilgileri);
        }

        // GET: /MucellithaneProgramNoPaketBilgileri/Create
        public ActionResult Create()
        {
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            return View();
        }

        // POST: /MucellithaneProgramNoPaketBilgileri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MucellithaneProgramNoPaketBilgileriId,IsEmriId,ProgramNo,IsKesimiProgramNo,PaketBilgileri")] MucellithaneProgramNoPaketBilgileri mucellithaneprogramnopaketbilgileri)
        {
            if (ModelState.IsValid)
            {
                db.MucellithaneProgramNoPaketBilgileri.Add(mucellithaneprogramnopaketbilgileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", mucellithaneprogramnopaketbilgileri.IsEmriId);
            return View(mucellithaneprogramnopaketbilgileri);
        }

        // GET: /MucellithaneProgramNoPaketBilgileri/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MucellithaneProgramNoPaketBilgileri mucellithaneprogramnopaketbilgileri = db.MucellithaneProgramNoPaketBilgileri.Find(id);
            if (mucellithaneprogramnopaketbilgileri == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", mucellithaneprogramnopaketbilgileri.IsEmriId);
            return View(mucellithaneprogramnopaketbilgileri);
        }

        // POST: /MucellithaneProgramNoPaketBilgileri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MucellithaneProgramNoPaketBilgileriId,IsEmriId,ProgramNo,IsKesimiProgramNo,PaketBilgileri")] MucellithaneProgramNoPaketBilgileri mucellithaneprogramnopaketbilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mucellithaneprogramnopaketbilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", mucellithaneprogramnopaketbilgileri.IsEmriId);
            return View(mucellithaneprogramnopaketbilgileri);
        }

        // GET: /MucellithaneProgramNoPaketBilgileri/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MucellithaneProgramNoPaketBilgileri mucellithaneprogramnopaketbilgileri = db.MucellithaneProgramNoPaketBilgileri.Find(id);
            if (mucellithaneprogramnopaketbilgileri == null)
            {
                return HttpNotFound();
            }
            return View(mucellithaneprogramnopaketbilgileri);
        }

        // POST: /MucellithaneProgramNoPaketBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MucellithaneProgramNoPaketBilgileri mucellithaneprogramnopaketbilgileri = db.MucellithaneProgramNoPaketBilgileri.Find(id);
            db.MucellithaneProgramNoPaketBilgileri.Remove(mucellithaneprogramnopaketbilgileri);
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
