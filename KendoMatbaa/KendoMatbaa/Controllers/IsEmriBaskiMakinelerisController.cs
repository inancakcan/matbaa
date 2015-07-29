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
    public class IsEmriBaskiMakinelerisController : BaseController
    {
        private matbaaEntities db = new matbaaEntities();

        // GET: IsEmriBaskiMakineleris
        public ActionResult Index()
        {
            var isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Include(i => i.BaskiEbatlari).Include(i => i.BaskiEbatlari1).Include(i => i.BaskiMakineleri).Include(i => i.IsEmri);
            return View(isEmriBaskiMakineleri.ToList());
        }

        public ActionResult _BelirliIsEmrininBaskiBilgileri(int IsEmriId,bool IcMi)
        {
            //if (IcMi)
            //{
            //    var isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Include(i => i.BaskiEbatlari).Include(i => i.BaskiEbatlari1).Include(i => i.BaskiMakineleri).Include(i => i.IsEmri).Where(i => i.IsEmriId == IsEmriId && i.MakineninKullanildigiYer == "Ic");
            //    return View(isEmriBaskiMakineleri.ToList());
            //}
            //else
            //{
            //    var isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Include(i => i.BaskiEbatlari).Include(i => i.BaskiEbatlari1).Include(i => i.BaskiMakineleri).Include(i => i.IsEmri).Where(i => i.IsEmriId == IsEmriId && i.MakineninKullanildigiYer == "Kapak");
            //    return View(isEmriBaskiMakineleri.ToList());
            //}


            var isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Include(i => i.BaskiEbatlari).Include(i => i.BaskiEbatlari1).Include(i => i.BaskiMakineleri).Include(i => i.IsEmri).Where(i => i.IsEmriId == IsEmriId);
            return View(isEmriBaskiMakineleri.ToList());

        }

        // GET: IsEmriBaskiMakineleris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriBaskiMakineleri isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Find(id);
            if (isEmriBaskiMakineleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriBaskiMakineleri);
        }

        // GET: IsEmriBaskiMakineleris/Create
        public ActionResult Create(string SeciliIsEmriId)
        {

            ViewBag.SeciliIsEmriId = SeciliIsEmriId;

            //ViewBag.IcBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut",BaskiIcinKapakveIcEbadiDon(int.Parse(SeciliIsEmriId), false));
            //ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", BaskiIcinKapakveIcEbadiDon(int.Parse(SeciliIsEmriId), true));


            //Pzt günü burayı kullanilacak kagittaki seçilmiş baski ebadi ile seçili olarak getir...
            Query q = new Query();
            IsEmriKullanilacakKagit isEmriKullanilacakKagit =
                q.IsEmriIdedenKullanilacakKagitBaskiBoyutlariDon(int.Parse(SeciliIsEmriId));



            ViewBag.IcBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut",isEmriKullanilacakKagit.IcBaskiBoyutu.Value);
            if (isEmriKullanilacakKagit.KapakBaskiBoyutu != null)
            {
                ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut",
                    isEmriKullanilacakKagit.KapakBaskiBoyutu.Value);
            }
            else
            {
                ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut","-");
            }
            
            /*
            ViewBag.IcBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut");
            ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut");
            */

            


            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi");
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", SeciliIsEmriId);
            //ViewBag.IcBaskiBoyutu = BaskiIcinKapakveIcEbadiDon(int.Parse(SeciliIsEmriId), false);
            //ViewBag.KapakBaskiBoyutu = BaskiIcinKapakveIcEbadiDon(int.Parse(SeciliIsEmriId), true);
            //Burada  bir yerde Baski boyutunu Kullanılacakkagit kesim boyutlar(iç ve kapak) ile eşleştirmek gerekli


            return View();
        }

        // POST: IsEmriBaskiMakineleris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsEmriBaskiMakineleriId,IsEmriId,BaskiMakineleriId,IcBaskiMiktari,KapakBaskiMiktari,IcBaskiEbadi,KapakBaskiEbadi,MakineninKullanildigiYer")] IsEmriBaskiMakineleri isEmriBaskiMakineleri)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.IsEmriBaskiMakineleri.Add(isEmriBaskiMakineleri);
                    db.SaveChanges();
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriBaskiMakineleri.IsEmriId, index = 6 }).Success("Baskı makinesi bilgisi ekleme işlemi başarılı");
                }

                ViewBag.IcBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriBaskiMakineleri.IcBaskiEbadi);
                ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriBaskiMakineleri.KapakBaskiEbadi);
                ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi", isEmriBaskiMakineleri.BaskiMakineleriId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriBaskiMakineleri.IsEmriId);
                return View(isEmriBaskiMakineleri);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriBaskiMakineleri.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriBaskiMakineleris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriBaskiMakineleri isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Find(id);
            if (isEmriBaskiMakineleri == null)
            {
                return HttpNotFound();
            }
            ViewBag.IcBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriBaskiMakineleri.IcBaskiEbadi);
            ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriBaskiMakineleri.KapakBaskiEbadi);
            ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi", isEmriBaskiMakineleri.BaskiMakineleriId);
            ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriBaskiMakineleri.IsEmriId);
            return View(isEmriBaskiMakineleri);
        }

        // POST: IsEmriBaskiMakineleris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IsEmriBaskiMakineleriId,IsEmriId,BaskiMakineleriId,IcBaskiMiktari,KapakBaskiMiktari,IcBaskiEbadi,KapakBaskiEbadi,MakineninKullanildigiYer")] IsEmriBaskiMakineleri isEmriBaskiMakineleri)
        {
            if (PersonelBelirliBirRoleSahipMi("MatbaaAdmin"))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(isEmriBaskiMakineleri).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriBaskiMakineleri.IsEmriId, index = 6 }).Success("Baskı makinesi bilgisi güncelleme işlemi başarılı");
                }
                ViewBag.IcBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriBaskiMakineleri.IcBaskiEbadi);
                ViewBag.KapakBaskiEbadi = new SelectList(db.BaskiEbatlari, "BaskiEbatlariId", "Boyut", isEmriBaskiMakineleri.KapakBaskiEbadi);
                ViewBag.BaskiMakineleriId = new SelectList(db.BaskiMakineleri, "BaskiMakineleriId", "MakineAdi", isEmriBaskiMakineleri.BaskiMakineleriId);
                ViewBag.IsEmriId = new SelectList(db.IsEmri, "IsEmriId", "IsNo", isEmriBaskiMakineleri.IsEmriId);
                return View(isEmriBaskiMakineleri);
            }
            else
            {
                return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriBaskiMakineleri.IsEmriId }).Warning("Yetkiniz yok!");
            }
        }

        // GET: IsEmriBaskiMakineleris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IsEmriBaskiMakineleri isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Find(id);
            if (isEmriBaskiMakineleri == null)
            {
                return HttpNotFound();
            }
            return View(isEmriBaskiMakineleri);
        }

        // POST: IsEmriBaskiMakineleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IsEmriBaskiMakineleri isEmriBaskiMakineleri = db.IsEmriBaskiMakineleri.Find(id);
            db.IsEmriBaskiMakineleri.Remove(isEmriBaskiMakineleri);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("TabView", "IsEmri", new { IsEmriId = isEmriBaskiMakineleri.IsEmriId, index = 6 }).Success("Baskı makinesi bilgisi silme işlemi başarılı");
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
