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
    public class IsEmriTeknikBilgilersController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriTeknikBilgilers
        public ActionResult Index()
        {
            var isEmriTeknikBilgiler = db.IsEmriTeknikBilgiler.Include(i => i.CiltTipleri).Include(i => i.IsCinsi).Include(i => i.IsEmri).Include(i => i.BaskiEbatlari).Include(i=>i.BitmisIsBoyutlari);
            return View(isEmriTeknikBilgiler.ToList());
        }

        // GET: IsEmriTeknikBilgilers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriTeknikBilgiler isEmriTeknikBilgiler = db.IsEmriTeknikBilgiler.Find(id);
            if (isEmriTeknikBilgiler == null)
            {
                return HttpNotFound();
            }
            return View(isEmriTeknikBilgiler);
        }

        // GET: IsEmriTeknikBilgilers/Create
        public ActionResult Create()
        {
            ViewBag.Cilt = new SelectList(db.CiltTipleri, "CiltTipleriId", "CiltTipiAdi");
            ViewBag.IsCinsiId = new SelectList(db.IsCinsi, "IsCinsiId", "CinsAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo");
            ViewBag.BaskiEbatlariId = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "BaskiEbatlari1");
            return View();
        }

        // POST: IsEmriTeknikBilgilers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriId,KesimBoyutlariId,Adedi,Cilt,SayfaSayisi,IcBaskiRenk,KapakBaskiRenk,IsEmriTeknikBilgilerId,IsCinsiId,Silindi,BitmisIsBoyutlariId")] IsEmriTeknikBilgiler isEmriTeknikBilgiler)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.IsEmriTeknikBilgiler.Add(isEmriTeknikBilgiler);
                    db.SaveChanges();
                    return RedirectToAction("Index", "IsEmri").Success("Teknik bilgilerin eklenmesi işlemi başarılı");
                }

                ViewBag.Cilt = new SelectList(db.CiltTipleri, "CiltTipleriId", "CiltTipiAdi", isEmriTeknikBilgiler.Cilt);
                ViewBag.IsCinsiId = new SelectList(db.IsCinsi, "IsCinsiId", "CinsAdi", isEmriTeknikBilgiler.IsCinsiId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriTeknikBilgiler.IsEmriId);
                ViewBag.KesimBoyutlariId = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriTeknikBilgiler.BaskiEbatlari.BaskiEbatlariId);
                return View(isEmriTeknikBilgiler);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriTeknikBilgiler.IsEmriId, index = 1 }).Danger("İş Emri bilgisi ekleme yetkisine sahip değilsiniz!");
            }
        }

        // GET: IsEmriTeknikBilgilers/Edit/5
        public ActionResult Edit(int? id)
        {
            //id yerine indexten IsEmriId bilgisi geliyor..bunu IsEmriTeknikBilgilerId bilgisine cevirmek gerekli
             //Once IsEmriTeknikBilgiler Tablsoıunda o işemrine ait kayıt var mı bak baklım
            if (IsEmriTeknikBilgilerKaydiVarmi(id.Value))
            {
                id = FindIsEmriTeknikBilgilerByIsEmriId(id.Value);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                IsEmriTeknikBilgiler isEmriTeknikBilgiler = db.IsEmriTeknikBilgiler.Find(id);
                if (isEmriTeknikBilgiler == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Cilt = new SelectList(db.CiltTipleri, "CiltTipleriId", "CiltTipiAdi", isEmriTeknikBilgiler.Cilt);
                ViewBag.IsCinsiId = new SelectList(db.IsCinsi.Where(i => i.Silindi == false), "IsCinsiId", "CinsAdi", isEmriTeknikBilgiler.IsCinsiId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriTeknikBilgiler.IsEmriId);
                ViewBag.KesimBoyutlariId = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriTeknikBilgiler.BaskiEbatlari.BaskiEbatlariId);
                ViewBag.BitmisIsBoyutlariId = new SelectList(db.BitmisIsBoyutlari, "Id", "BitmisIsBoyutuAdi", isEmriTeknikBilgiler.BitmisIsBoyutlariId);
                return View(isEmriTeknikBilgiler);
            }
            else
            {
                //İş Emrine ait Teknik bilgiler bilgisi bulunamadı
                return RedirectToAction("TabView", "IsEmri", new { id = id.Value, index = 1 }).Warning("İş Emrine ait teknik bilgiler bilgisi bulunamadı");
            }
        }

        // POST: IsEmriTeknikBilgilers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriId,KesimBoyutlariId,Adedi,Cilt,SayfaSayisi,IcBaskiRenk,KapakBaskiRenk,IsEmriTeknikBilgilerId,IsCinsiId,Silindi,BitmisIsBoyutlariId")] IsEmriTeknikBilgiler isEmriTeknikBilgiler)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                int iIsEmriId = FindIsEmriIdByIsEmriTeknikBilgilerId(isEmriTeknikBilgiler.IsEmriTeknikBilgilerId);
                    isEmriTeknikBilgiler.IsEmriId=iIsEmriId;
                isEmriTeknikBilgiler.Silindi = false;
                if (ModelState.IsValid)
                {
                    db.Entry(isEmriTeknikBilgiler).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriTeknikBilgiler.IsEmriId, index = 1 }).Success("Teknik bilgilerin güncellenmesi işlemi başarılı");
                }
                ViewBag.Cilt = new SelectList(db.CiltTipleri, "CiltTipleriId", "CiltTipiAdi", isEmriTeknikBilgiler.Cilt);
                ViewBag.IsCinsiId = new SelectList(db.IsCinsi.Where(i => i.Silindi == false), "IsCinsiId", "CinsAdi", isEmriTeknikBilgiler.IsCinsiId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriTeknikBilgiler.IsEmriId);
                ViewBag.BaskiEbatlariId = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "BaskiEbatlari1", isEmriTeknikBilgiler.BaskiEbatlari.BaskiEbatlariId);
                ViewBag.BitmisIsBoyutlariId = new SelectList(db.BitmisIsBoyutlari, "Id", "BitmisIsBoyutuAdi", isEmriTeknikBilgiler.BitmisIsBoyutlariId);return View(isEmriTeknikBilgiler);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriTeknikBilgiler.IsEmriId, index = 1 }).Danger("İş Emri bilgisi güncelleme yetkisine sahip değilsiniz!");
            }
        }

        // GET: IsEmriTeknikBilgilers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriTeknikBilgiler isEmriTeknikBilgiler = db.IsEmriTeknikBilgiler.Find(id);
            if (isEmriTeknikBilgiler == null)
            {
                return HttpNotFound();
            }
            return View(isEmriTeknikBilgiler);
        }

        // POST: IsEmriTeknikBilgilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriTeknikBilgiler isEmriTeknikBilgiler = db.IsEmriTeknikBilgiler.Find(id);
            db.IsEmriTeknikBilgiler.Remove(isEmriTeknikBilgiler);
            db.SaveChanges();
            return RedirectToAction("Index", "IsEmri").Success("Teknik bilgilerin silinmesi işlemi başarılı");
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
