using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KendoMatbaa.Models;
using KendoMatbaa.App_Start;

namespace KendoMatbaa.Controllers
{
    public class IsEmriOnayBilgilerisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriOnayBilgileris
        public ActionResult Index()
        {
            var isEmriOnayBilgileri = db.IsEmriOnayBilgileri.Include(i => i.IsEmri).Include(i => i.IsEmri1).Include(i => i.IsEmri2);
            return View(isEmriOnayBilgileri.ToList());
        }

        // GET: IsEmriOnayBilgileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriOnayBilgileri isEmriOnayBilgileri = db.IsEmriOnayBilgileri.Find(id);
            if (isEmriOnayBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriOnayBilgileri);
        }

        // GET: IsEmriOnayBilgileris/Create
        public ActionResult Create()
        {
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            return View();
        }

        // POST: IsEmriOnayBilgileris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriID,Otokopi,Aciklama,Onayalindi,Reddedildi,RedNedeni,AdetAciklama,IsTamamlandi,OnayGeriAlindi,KismiTamam,YarimKaldi,IsEmriBaskiBilgileriId,Silindi")] IsEmriOnayBilgileri isEmriOnayBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.IsEmriOnayBilgileri.Add(isEmriOnayBilgileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
            return View(isEmriOnayBilgileri);
        }

        // GET: IsEmriOnayBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {

            //id yerine indexten IsEmriId bilgisi geliyor..bunu IsEmriBaskiBilgileri bilgisine cevirmek gerekli
            //Once IsEmriBaskiBilgilerTablsoıunda o işemrine ait kayıt var mı bak baklım
            if (IsEmriBaskiBilgileriKaydiVarmi(id.Value))
            {
                id = FindIsEmriBaskiBilgileriByIsEmriId(id.Value);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IsEmriOnayBilgileri isEmriOnayBilgileri = db.IsEmriOnayBilgileri.Find(id);
                if (isEmriOnayBilgileri == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
                ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
                ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
                return View(isEmriOnayBilgileri);
            }
            else
            {
                //İş Emrine ait baskı bilgisi bulunamadı
                return RedirectToAction("Index", "IsEmri").Warning("İş Emrine ait baskı bilgisi bulunamadı");
            }
        }

        // POST: IsEmriOnayBilgileris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriID,Otokopi,Aciklama,Onayalindi,Reddedildi,RedNedeni,AdetAciklama,IsTamamlandi,OnayGeriAlindi,KismiTamam,YarimKaldi,IsEmriBaskiBilgileriId,Silindi")] IsEmriOnayBilgileri isEmriOnayBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(isEmriOnayBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "IsEmri").Success("Onay bilgilerinin güncellenmesi başarılı");
            }
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
            ViewBag.IsEmriID = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriOnayBilgileri.IsEmriID);
            return RedirectToAction("Index", "IsEmri").Danger("Onay bilgilerinin güncellenmesi başarısız!");
        }

        // GET: IsEmriOnayBilgileris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriOnayBilgileri isEmriOnayBilgileri = db.IsEmriOnayBilgileri.Find(id);
            if (isEmriOnayBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriOnayBilgileri);
        }

        // POST: IsEmriOnayBilgileris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriOnayBilgileri isEmriOnayBilgileri = db.IsEmriOnayBilgileri.Find(id);
            db.IsEmriOnayBilgileri.Remove(isEmriOnayBilgileri);
            db.SaveChanges();
            return RedirectToAction("Index", "IsEmri").Success("Onay bilgilerinin silinmesi başarılı");
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
